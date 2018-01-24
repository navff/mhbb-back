﻿
using API.Models;
using API.ViewModels;
using AutoMapper;
using Models.DTO;
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
                cfg.CreateMap<Picture, PictureViewModelShortGet>();
                cfg.CreateMap<PictureShortDTO, PictureViewModelShortGet>();
                cfg.CreateMap<City, CityViewModelGet>();
                cfg.CreateMap<Organizer, OrganizerViewModelGet>();
                cfg.CreateMap<Interest, InterestViewModelGet>();
                cfg.CreateMap<TempFile, TempFileViewModelGet>();
                cfg.CreateMap<Activity, ActivityViewModelGet>();
                cfg.CreateMap<Activity, ActivityViewModelShortGet>();
                cfg.CreateMap<Reservation, ReservationViewModelGet>();
                cfg.CreateMap<Review, ReviewViewModelGet>();



                // ViewModels to Models
                cfg.CreateMap<UserViewModelPut, User>();
                cfg.CreateMap<PictureViewModelGet, Picture>();
                cfg.CreateMap<OrganizerViewModelPost, Organizer>();
                cfg.CreateMap<InterestViewModelPost, Interest>();
                cfg.CreateMap<TempFileViewModelPost, TempFile>();
                cfg.CreateMap<ActivityViewModelPost, Activity>();
                cfg.CreateMap<ActivityViewModelPut, Activity>();
                cfg.CreateMap<ReservationViewModelPost, Reservation>();
                cfg.CreateMap<ReviewViewModelPost, Review>();
            });
        }
    }
}