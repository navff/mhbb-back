using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;
using Models.Tools;

namespace Models.Operations
{
    public class TempFileOperations
    {
        public async Task<TempFile> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TempFile> UpdateAsync(TempFile tempFile)
        {
            throw new NotImplementedException();
        }

        public async Task<TempFile> AddAsync(TempFile tempFile)
        {
            tempFile.Data = Images.Resize(1000, tempFile.Data);
            throw new NotImplementedException();
            
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TempFile> GetBySessionIdAsync(int sessionId)
        {
            throw new NotImplementedException();
        }

        public async Task<TempFile> GetByFormIdAsync(int formId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAllBySessionIdAsync(string sessionId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAllByFormIdAsync(string sessionId)
        {
            throw new NotImplementedException();
        }
    }
}
