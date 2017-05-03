﻿
using API.Models;
using API.ViewModels;
using AutoMapper;
using Models.Entities;

namespace API
{
    /// <summary>
    /// Маппинг для Automapper
    /// </summary>
    public class MapperMappings
    {
        /// <summary>
        /// Главный метод
        /// </summary>
        public static void Map()
        {
            Mapper.Initialize(cfg => {
                
                // Model to ViewModels
                cfg.CreateMap<User, UserViewModelPut>();
                cfg.CreateMap<User, UserViewModelGet>();
                cfg.CreateMap<Picture, PictureViewModelGet>();
                cfg.CreateMap<City, CityViewModelGet>();
                cfg.CreateMap<Organizer, OrganizerViewModelGet>();
                cfg.CreateMap<Interest, InterestViewModelGet>();
                cfg.CreateMap<TempFile, TempFileViewModelGet>();
                cfg.CreateMap<Activity, ActivityViewModelGet>();



                // ViewModels to Models
                cfg.CreateMap<UserViewModelPut, User>();
                cfg.CreateMap<PictureViewModelGet, Picture>();
                cfg.CreateMap<OrganizerViewModelPost, Organizer>();
                cfg.CreateMap<InterestViewModelPost, Interest>();
                cfg.CreateMap<TempFileViewModelPost, TempFile>();
                cfg.CreateMap<ActivityViewModelPost, Activity>();
            });
        }
    }
}