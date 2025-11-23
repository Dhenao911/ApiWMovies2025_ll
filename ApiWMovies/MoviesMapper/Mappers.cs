using ApiWMovies.DAL.Dtos;
using ApiWMovies.DAL.Models;
using AutoMapper;

namespace ApiWMovies.MoviesMapper;

public class Mappers : Profile
{
    public Mappers()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CategoryUpdateCreateDto>().ReverseMap();
        CreateMap<Movie, MovieDto>().ReverseMap();
        CreateMap<Movie, MovieCreateUpdateDto>().ReverseMap();
    }
}