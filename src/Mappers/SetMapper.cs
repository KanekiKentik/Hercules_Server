public static class SetMapper
{
    public static SetResponseDTO ToDTO(this SetEntity set)
    {
        return new (set.Id, set.Weight, set.Reps, set.Order);
    }

    public static SetEntity ToEntity(this SetRequestDTO sInfo)
    {
        return new (sInfo.Weight, sInfo.Reps, sInfo.Order);
    }
}