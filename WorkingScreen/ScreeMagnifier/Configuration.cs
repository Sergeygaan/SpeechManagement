using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceControl
{
    /// <summary>
    /// Configuration class for the magnifier.
    /// </summary>
    public class Configuration
    {
        public Configuration()
        {
        }

        public float ZoomFactor
        {
            get { return mZoomFactor; }
            set
            {
                if (value > ZOOM_FACTOR_MAX)
                {
                    mZoomFactor = ZOOM_FACTOR_MAX;
                }
                else if (value < ZOOM_FACTOR_MIN)
                {
                    mZoomFactor = ZOOM_FACTOR_MIN;
                }
                else
                {
                    mZoomFactor = value;
                }
            }
        }
        private float mZoomFactor = ZOOM_FACTOR_DEFAULT;

        public float SpeedFactor
        {
            get { return mSpeedFactor; }
            set
            {
                if (value > SPEED_FACTOR_MAX)
                {
                    mSpeedFactor = SPEED_FACTOR_MAX;
                }
                else if (value < SPEED_FACTOR_MIN)
                {
                    mSpeedFactor = SPEED_FACTOR_MIN;
                }
                else
                {
                    mSpeedFactor = value;
                }
            }
        }
        private float mSpeedFactor = SPEED_FACTOR_DEFAULT;


        public int LocationX = -1;
        public int LocationY = -1;

        public bool CloseOnMouseUp = true;
        public bool DoubleBuffered = true;
        public bool HideMouseCursor = true;
        public bool RememberLastPoint = true;
        public bool ReturnToOrigin = true;
        public bool ShowInTaskbar = false;
        public bool TopMostWindow = true;

        public int MagnifierWidth = 150;
        public int MagnifierHeight = 150;

        public static readonly float ZOOM_FACTOR_MAX = 1.0f;
        public static readonly float ZOOM_FACTOR_MIN = 1.0f;
        public static readonly float ZOOM_FACTOR_DEFAULT = 1.0f;

        public static readonly float SPEED_FACTOR_MAX = 1.0f;
        public static readonly float SPEED_FACTOR_MIN = 0.05f;
        public static readonly float SPEED_FACTOR_DEFAULT = 0.35f;
    }
}
