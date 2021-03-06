﻿namespace SoundFingerprinting.Tests.Unit.FFT
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SoundFingerprinting.Configuration;
    using SoundFingerprinting.FFT;

    [TestClass]
    public class LogUtilityTest : AbstractTest
    {
        private readonly ILogUtility logUtility = new LogUtility();

        [TestMethod]
        public void FrequencyToIndexTest()
        {
            int result = logUtility.FrequencyToSpectrumIndex(318, 5512, 2048);

            Assert.AreEqual((318 * 1024) / (5512 / 2), result);
        }

        [TestMethod]
        public void GenerateLogFrequenciesRangesTest()
        {
            var defaultConfig = new CustomSpectrogramConfig { UseDynamicLogBase = false, LogBase = 10 };
            float[] logSpacedFrequencies = new[] // generated in matlab with logspace(2.50242712, 3.3010299957, 33)
                {
                    318.00f, 336.81f, 356.73f, 377.83f, 400.18f, 423.85f, 448.92f, 475.47f, 503.59f, 533.38f, 564.92f,
                    598.34f, 633.73f, 671.21f, 710.91f, 752.96f, 797.50f, 844.67f, 894.63f, 947.54f, 1003.58f, 1062.94f,
                    1125.81f, 1192.40f, 1262.93f, 1337.63f, 1416.75f, 1500.54f, 1589.30f, 1683.30f, 1782.86f, 1888.31f,
                    2000f
                };

            int[] indexes = logUtility.GenerateLogFrequenciesRanges(FingerprintConfiguration.Default.SampleRate, defaultConfig);

            for (int i = 0; i < logSpacedFrequencies.Length; i++)
            {
                var logSpacedFrequency = logSpacedFrequencies[i];
                int index = logUtility.FrequencyToSpectrumIndex(logSpacedFrequency, FingerprintConfiguration.Default.SampleRate, defaultConfig.WdftSize);
                Assert.AreEqual(index, indexes[i]);
            }
        }
    }
}
