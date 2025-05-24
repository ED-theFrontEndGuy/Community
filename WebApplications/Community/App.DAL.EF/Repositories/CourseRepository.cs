using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class CourseRepository : BaseRepository<CourseDto, Course>, ICourseRepository
{
    public CourseRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new CourseUOWMapper())
    {
    }
}