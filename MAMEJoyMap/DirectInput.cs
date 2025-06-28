using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Timers;
using DInput = Microsoft.DirectX.DirectInput;

namespace MAMEJoyMap
{
    public class DirectInput : IDisposable
    {
        public enum ForceType
        {
            VeryBriefJolt,
            BriefJolt,
            LowRumble,
            HardRumble
        }

        [DllImport("user32.dll", SetLastError = false)]
        private static extern IntPtr GetDesktopWindow();

        private DInput.Device[] m_joyDevice = null;

        private int m_joyCount = 0;
        private System.Timers.Timer m_timer = null;

        private const int MAX_JOYSTICKS = 8;

        private const int AXIS_MIN = -255;
        private const int AXIS_NONE = 0;
        private const int AXIS_MAX = 255;

        public delegate void DIJoyButtonDownHandler(object sender, JoyEventArgs e);

        public event DIJoyButtonDownHandler OnJoyInput = null;

        public DirectInput(System.Windows.Forms.Control control)
        {
            try
            {
                DInput.DeviceList gameControllerList = DInput.Manager.GetDevices(DInput.DeviceClass.GameControl, DInput.EnumDevicesFlags.AttachedOnly);

                m_joyDevice = new Microsoft.DirectX.DirectInput.Device[MAX_JOYSTICKS];

                foreach (DInput.DeviceInstance deviceInstance in gameControllerList)
                {
                    if (m_joyCount == MAX_JOYSTICKS)
                        break;

                    m_joyDevice[m_joyCount] = new DInput.Device(deviceInstance.InstanceGuid);
                    m_joyDevice[m_joyCount].SetCooperativeLevel(GetDesktopWindow(), DInput.CooperativeLevelFlags.Background | DInput.CooperativeLevelFlags.NonExclusive);
                    m_joyDevice[m_joyCount].SetDataFormat(DInput.DeviceDataFormat.Joystick);

                    m_joyDevice[m_joyCount].Properties.AxisModeAbsolute = true;

                    SetAxisRange(m_joyDevice[m_joyCount]);

                    m_joyDevice[m_joyCount].Acquire();
                    m_joyDevice[m_joyCount].Poll();

                    m_joyCount++;
                }

                m_timer = new System.Timers.Timer(50);
                m_timer.Elapsed += new ElapsedEventHandler(OnTimerElapsed);
                m_timer.SynchronizingObject = control;
                m_timer.Start();
            }
            catch
            {
            }
        }

        private void SetAxisRange(DInput.Device joyDevice)
        {
            foreach (DInput.DeviceObjectInstance doi in joyDevice.Objects)
            {
                if ((doi.ObjectId & (int)DInput.DeviceObjectTypeFlags.Axis) != 0)
                    joyDevice.Properties.SetRange(DInput.ParameterHow.ById, doi.ObjectId, new DInput.InputRange(AXIS_MIN, AXIS_MAX));
            }
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            PollJoystick();
        }

        public void PollJoystick()
        {
            if (OnJoyInput == null)
                return;

            try
            {
                for (int i = 0; i < m_joyCount; i++)
                {
                    if (m_joyDevice[i] != null)
                    {
                        DInput.JoystickState joyState;

                        try
                        {
                            m_joyDevice[i].Poll();

                            joyState = m_joyDevice[i].CurrentJoystickState;
                        }
                        catch
                        {
                            m_joyDevice[i].Unacquire();
                            m_joyDevice[i].Acquire();

                            continue;
                        }

                        OnJoyInput(this, new JoyEventArgs(i, joyState));
                    }
                }
            }
            catch
            {
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (m_timer != null)
            {
                m_timer.Stop();
                m_timer.Dispose();
                m_timer = null;
            }

            for (int i = 0; i < m_joyCount; i++)
            {
                if (m_joyDevice[i] != null)
                {
                    m_joyDevice[i].Unacquire();
                    m_joyDevice[i].Dispose();
                    m_joyDevice[i] = null;
                }
            }
        }

        #endregion

    }

    public class JoyEventArgs : EventArgs
    {
        public int JoyId = 0;
        public DInput.JoystickState JoyState;

        public JoyEventArgs(int joyId, DInput.JoystickState joyState)
        {
            JoyId = joyId;
            this.JoyState = joyState;
        }
    }
}
