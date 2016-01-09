using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Domain;
using eUni.DataAccess.Repository;

namespace eUni.BusinessLogic.Providers
{
    public class WikiPageProvider : IWikiPageProvider
    {
        private readonly IWikiPageRepository _wikiPageRepo;
        private readonly Repository<Module> _moduleRepo;

        public WikiPageProvider( IWikiPageRepository wikiPageRepo, Repository<Module> moduleRepo)
        {
            _wikiPageRepo = wikiPageRepo;
            _moduleRepo = moduleRepo;
        }


        public void CreateWikiPage(WikiPageDTO dtoWikiPage)
        {
            var wikiPage = Mapper.Map<WikiPage>(dtoWikiPage);
            var module = _moduleRepo.Get(x => x.ModuleId == dtoWikiPage.Module.ModuleId);
            wikiPage.Module = module;
            _wikiPageRepo.Add(wikiPage);
        }
    }
}
