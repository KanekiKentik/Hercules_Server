public static class MuscleGroupMapper
{
    public static MuscleGroupDTO ToDTO(this MuscleGroupEntity muscle)
    {
        return new (muscle.Id, muscle.Name);
    } 
}