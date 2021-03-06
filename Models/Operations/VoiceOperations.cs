﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;

namespace Models.Operations
{
    public class VoiceOperations
    {
        private HobbyContext _context;

        public VoiceOperations(HobbyContext context)
        {
            _context = context;
        }

        public async Task<int> AddVoice(int userId, VoiceValue voiceValue, int activityId)
        {
            var voice = _context.ActivityUserVoices
                            .FirstOrDefault(v => (v.ActivityId == activityId)
                                                 && (v.UserId == userId)) 
                        ?? new ActivityUserVoice
                        {
                            ActivityId = activityId,
                            UserId = userId,
                        };
            voice.VoiceValue = voiceValue;
            if (voice.Id == 0)
            {
                _context.ActivityUserVoices.Add(voice);
            }
            await _context.SaveChangesAsync();
            return await GetActivityVoices(activityId);
        }

        public async Task<int> GetActivityVoices(int activityId)
        {
            var voices = await _context.ActivityUserVoices
                                 .Where(v => v.ActivityId == activityId)
                                 .Select(v => v.VoiceValue).ToListAsync();

            var result = 0;
            foreach (var voice in voices)
            {
                result += (int) voice;
            }
            return result;
        }

        public async Task<IEnumerable<ActivityUserVoice>> GetUserVoices(int userId)
        {
            return await _context.ActivityUserVoices
                                 .Where(v => v.UserId == userId)
                                 .ToListAsync();

        }

        public async Task DeleteVoiceAsync(int id)
        {
            var voice = await _context.ActivityUserVoices.FirstAsync( v => v.Id == id);
            _context.ActivityUserVoices.Remove(voice);
        }
    }
}
