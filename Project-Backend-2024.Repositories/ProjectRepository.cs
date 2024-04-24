﻿using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Repositories;

public class ProjectRepository : RepositoryBase<Project>, IRepositoryBase<Project>
{
    public ProjectRepository(DatabaseContext context) : base(context) { }
}
