﻿using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class Skill : IEntity
{
    public int Id { get; }
    public string Name { get; init; } = null!;
}
