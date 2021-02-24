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
        private const string USER_AGENT = "Core Service";
        public WebContentService(ContentDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _WebContentDbSet = context.Set<WebContent>();
            _identityService = serviceProvider.GetService<IIdentityService>();
        
    }

        public Task<int> Create(WebContent model)
        {
            _WebContentDbSet.Add(model);
            return _context.SaveChangesAsync();
        }

        public List<WebContent> Find()
        {
            return _context.WebContents.ToList();
        }

        public Task<List<WebContent>> FindAsync()
        {
            return _context.WebContents.ToListAsync();
        }

        public Task<WebContent> GetSingleById(int id)
        {
            return _WebContentDbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> Update(WebContent dbmodel, WebContent model)
        {
            dbmodel.Name = model.Name;
            dbmodel.Title = model.Title;
            dbmodel.Description = model.Description;
            dbmodel.ImageUrl = model.ImageUrl;
            dbmodel.Link = model.Link;
            dbmodel.Order = model.Order;
            dbmodel.LinkYoutube = model.LinkYoutube;
            dbmodel.TextButton = model.TextButton;
            return _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var model = await GetSingleById(id);

            if (model == null)
                throw new Exception("Invalid Id");

            EntityExtension.FlagForDelete(model, "test", "test");
            _WebContentDbSet.Update(model);
            return await _context.SaveChangesAsync();
        }
    }
}
