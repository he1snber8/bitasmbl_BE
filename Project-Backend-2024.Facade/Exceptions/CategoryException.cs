namespace Project_Backend_2024.Facade.Exceptions;

public class ProjectCategoryNotSelectedException() : Exception("Category field is required");

public class ProjectRequirementNotSelectedException() : Exception("Requirement field is required");

public class ProjectMediaNotIncludedException() :  Exception("Images are necessary to attract more interest");