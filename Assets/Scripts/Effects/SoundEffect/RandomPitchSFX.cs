public class RandomPitchSFX : SoundEffect
{
    private void OnEnable() {
        audioSource.pitch = UnityEngine.Random.Range(1f, 1.3f);
    }
}