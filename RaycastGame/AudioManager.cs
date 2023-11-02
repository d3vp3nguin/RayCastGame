using SFML.Audio;

namespace RaycastGame
{
    public class AudioManager
    {
        private Music music;

        private SoundBuffer stepSoundBuffer;
        private Sound stepSound;

        private Player player;
        private float stepTimer;

        public AudioManager(Player player)
        {
            this.player = player;

            music = new Music(Config.MusicPath);
            music.Loop = true;
            music.Play();

            stepSoundBuffer = new SoundBuffer(Config.StepSoundPath);
            stepSound = new Sound(stepSoundBuffer);
        }

        public void Update(float deltaTime)
        {
            if (stepTimer > 0) stepTimer-=deltaTime;
            else stepTimer=0;

            if (player.IsPlayerWalk && stepTimer <= 0)
            {
                stepSound.Play();
                stepTimer = Config.StepTime;
            }
        }

        public void ChangeVolume()
        {
            music.Volume = Settings.Volume;
            stepSound.Volume = Settings.Volume;
        }
    }
}
