using com.dyeingprinting.service.content.data;
using com.dyeingprinting.service.content.data.Model;
using EWorkplaceAbsensiService.Lib.Helpers.IdentityService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Moonlay.Models;

namespace com.dyeingprinting.service.content.business.Service
{
    public class MobileContentService : IService<MobileContent>
    {
        private readonly ContentDbContext _context;
        private readonly DbSet<MobileContent> _mobileContentsDbSet;
        private readonly IIdentityService _identityService;
        private const string USER_AGENT = "Core Service";
        public MobileContentService(ContentDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _mobileContentsDbSet = context.Set<MobileContent>();
            _identityService = serviceProvider.GetService<IIdentityService>();
        }

        public Task<int> Create(MobileContent model)
        {
            _mobileContentsDbSet.Add(model);
            return _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var model = await GetSingleById(id);

            if (model == null)
                throw new Exception("Invalid Id");

            EntityExtension.FlagForDelete(model, "test", "test");
            _mobileContentsDbSet.Update(model);
            return await _context.SaveChangesAsync();
        }

        public List<MobileContent> Find()
        {
            return _context.MobileContents.ToList();
        }

        public Task<List<MobileContent>> FindAsync()
        {
            return _context.MobileContents.ToListAsync();
        }

        public Task<MobileContent> GetSingleById(int id)
        {
            return _mobileContentsDbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> Update(MobileContent dbmodel, MobileContent model)
        {
            dbmodel.Name = model.Name;
            dbmodel.Title = model.Title;
            dbmodel.Description = model.Description;
            dbmodel.ImageUrl = model.ImageUrl;
            dbmodel.QueryUrl = model.QueryUrl;
            dbmodel.Order = model.Order;
            dbmodel.LinkYoutube = model.LinkYoutube;
            dbmodel.TextButton = model.TextButton;
            dbmodel.CategoryId = model.CategoryId;
            dbmodel.CategoryName = model.CategoryName;
            return _context.SaveChangesAsync();
        }
    }
}
