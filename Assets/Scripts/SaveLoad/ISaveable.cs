namespace ColonyBuilder.SaveLoad
{
    public interface ISaveable
    {
        object CaptureState();
        void RestoreState(object state);
    }

}
