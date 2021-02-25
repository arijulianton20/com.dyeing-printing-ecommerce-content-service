using com.dyeingprinting.service.content.data;
using com.dyeingprinting.service.content.data.Model;
using Com.Moonlay.Models;
using EWorkplaceAbsensiService.Lib.Helpers.IdentityService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.dyeingprinting.service.content.business.Service
{
    public class WebContentService : IService<WebContent>
    {
        private readonly ContentDbContext _context;
        private readonly DbSet<WebContent> _WebContentDbSet;
        private readonly IIdentityService _identityService;
        private const string _userBy = "DP-CONTENT";
        private const string _userAgent = "DP-CONTENT";

        public WebContentService(ContentDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _WebContentDbSet = context.Set<WebContent>();
            _identityService = serviceProvider.GetService<IIdentityService>();
        }

        public async Task<int> Create(WebContent model)
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                EntityExtension.FlagForCreate(model, _userBy, _userAgent);
                _context.WebContents.Add(model);
                var result = await _context.SaveChangesAsync();

                transaction.Commit();

                return result;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }

        public List<WebContent> Find()
        {
            return _context.WebContents.ToList();
        }

        public async Task<List<WebContent>> FindAsync()
        {
            return await _context.WebContents.ToListAsync();
        }

        public Task<WebContent> GetSingleById(int id)
        {
            return _WebContentDbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<int> Update(WebContent dbmodel, WebContent model)
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                dbmodel.UpdateData(model);
                EntityExtension.FlagForUpdate(dbmodel, _userBy, _userAgent);
                _context.WebContents.Update(dbmodel);
                var result = await _context.SaveChangesAsync();

                transaction.Commit();

                return result;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }

        public async Task<int> Delete(int id)
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                var model = await GetSingleById(id);

                if (model == null)
                    throw new Exception("Invalid Id");

                EntityExtension.FlagForDelete(model, _userBy, _userAgent);
                _WebContentDbSet.Update(model);
                var result = await _context.SaveChangesAsync();

                transaction.Commit();

                return result;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }
    }
}
