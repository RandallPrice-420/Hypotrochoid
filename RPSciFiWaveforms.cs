using System;


namespace Spirograph_v1
{
    public static class RPSciFiWaveforms
    {
        public static readonly Func<float, float> Sine =
            t => (float)Math.Sin(t * 2 * Math.PI);

        public static readonly Func<float, float> Square =
            t => Math.Sign(Math.Sin(t * 2 * Math.PI));

        public static readonly Func<float, float> Triangle =
            t =>
            {
                float v = (t % 1f) * 2f;
                return v < 1f ? v : 2f - v;
            };

        public static readonly Func<float, float> Sawtooth =
            t => (t % 1f) * 2f - 1f;

        public static readonly Func<float, float> Pulsar =
            t => (float)Math.Exp(-20 * Math.Abs(Math.Sin(t * Math.PI))) * 2f - 1f;

        public static readonly Func<float, float> Plasma =
            t => (float)(Math.Sin(t * 4) * Math.Cos(t * 7));

        public static readonly Func<float, float> Interference =
            t => (float)(Math.Sin(t * 6) + Math.Sin(t * 13)) * 0.5f;

        public static readonly Func<float, float> HarmonicStack =
            t => (float)(
                0.6 * Math.Sin(t * 2 * Math.PI) +
                0.3 * Math.Sin(t * 4 * Math.PI) +
                0.1 * Math.Sin(t * 8 * Math.PI));

        public static readonly Func<float, float> Heartbeat =
            t =>
            {
                float x = t % 1f;
                return x < 0.1f ? 1f - x * 10f : (float)Math.Sin((x - 0.1f) * 10f);
            };

        public static readonly Func<float, float> WarpField =
            t => (float)(Math.Sin(t * 3) * Math.Sin(t * 17) * Math.Cos(t * 5));

        public static readonly Func<float, float> AlienSignal =
            t => (float)(Math.Sin(t * 5 + Math.Sin(t * 0.5)) * Math.Cos(t * 0.2));


    }   // class RPSciFiWaveforms

}	// namespace Spirograph_v1
