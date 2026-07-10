public static class SessionExerciseMapper
{
    public static SessionExerciseResponseDTO ToDTO(this SessionExerciseEntity session)
    {
        var sets = session.Sets;
        if (sets is not { Count: > 0 })
            throw new InvalidOperationException($"Sets not included for SessionExercise with id: {session.Id}");

        return new (session.Id, session.ExerciseId, session.Order, session.Sets.Select(s => s.ToDTO()));
    }

    public static SessionExerciseEntity ToEntity(this SessionExerciseRequestDTO sInfo)
    {
        return new (sInfo.ExerciseId, sInfo.Order, sInfo.Sets.Select(s => s.ToEntity()));
    }
}