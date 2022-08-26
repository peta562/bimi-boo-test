namespace Infrastructure.Services.SoundManager {
    public interface ISoundManager : IService {
        void PlaySound(SoundType soundType);
    }
}