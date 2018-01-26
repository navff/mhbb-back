using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Tools;

namespace Models.Operations
{
    public class SystemToolsOperations
    {
        private HobbyContext _context;
        private PictureOperations _pictureOperations;

        public SystemToolsOperations(HobbyContext context, PictureOperations pictureOperations)
        {
            _context = context;
            _pictureOperations = pictureOperations;
        }

        public async Task ReformatOnePicture(int pictureId)
        {
            var picture = await  _pictureOperations.GetAsync(pictureId);
            picture.Data = Images.Resize(1000, picture.Data);
            await _pictureOperations.UpdateAsync(picture);
        }

        public async Task ReformatAllPictures()
        {
            var pictureIds = _context.Pictures.Select(p => p.Id).ToList();
            foreach (var id in pictureIds)
            {
                await ReformatOnePicture(id);
            }
        }

        public async Task RemoveAllTempFiles()
        {

            var tempFiles = _context.TempFiles;
            _context.TempFiles.RemoveRange(tempFiles);
            await _context.SaveChangesAsync();
        }


    }
}
