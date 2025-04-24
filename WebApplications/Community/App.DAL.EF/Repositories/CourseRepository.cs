using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class CourseRepository : BaseRepository<Course>, ICourseRepository
{
    public CourseRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}