using System;
using System.Collections.Generic;
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

        public async Task<ActivityUserVoice> AddVoice(string email, VoiceValue voiceValue, int activityId)
        {
            var voice = _context.ActivityUserVoices
                            .FirstOrDefault(v => (v.ActivityId == activityId)
                                                 && (v.UserEmail == email)) 
                        ?? new ActivityUserVoice
                        {
                            ActivityId = activityId,
                            UserEmail = email,
                        };
            voice.VoiceValue = voiceValue;
            if (voice.Id == 0)
            {
                _context.ActivityUserVoices.Add(voice);
            }
            await _context.SaveChangesAsync();
            return voice;
        }
    }
}
