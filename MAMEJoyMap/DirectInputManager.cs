using System;
using System.Timers;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SharpDX.DirectInput;

namespace MAMEJoyMap
{
    public class DirectInputManager : IDisposable
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

        private const int MAX_JOYSTICKS = 8;
        private const int AXIS_MIN = -255;
        private const int AXIS_NONE = 0;
        private const int AXIS_MAX = 255;

        private Joystick[] _joyDevice = new Joystick[MAX_JOYSTICKS];
        private int _joyCount = 0;
        private System.Timers.Timer _timer = null;
        private SharpDX.DirectInput.DirectInput _directInput;

        public delegate void DIJoyButtonDownHandler(object sender, JoyEventArgs e);
        public event DIJoyButtonDownHandler OnJoyInput = null;

        public DirectInputManager(Control control)
        {
            _directInput = new SharpDX.DirectInput.DirectInput();

            foreach (var deviceInstance in _directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly))
            {
                if (_joyCount == MAX_JOYSTICKS)
                    break;

                var joystick = new Joystick(_directInput, deviceInstance.InstanceGuid);

                foreach (DeviceObjectInstance deviceObject in joystick.GetObjects())
                {
                    if ((deviceObject.ObjectId.Flags & DeviceObjectTypeFlags.Axis) != 0)
                    {
                        var props = joystick.GetObjectPropertiesById(deviceObject.ObjectId);
                        props.Range = new InputRange(AXIS_MIN, AXIS_MAX);
                    }
                }

                joystick.Properties.AxisMode = DeviceAxisMode.Absolute;

                joystick.Acquire();

                _joyDevice[_joyCount] = joystick;
                _joyCount++;
            }

            _timer = new System.Timers.Timer(50);
            _timer.Elapsed += OnTimerElapsed;
            _timer.SynchronizingObject = control;
            _timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            PollJoystick();
        }

        public void PollJoystick()
        {
            if (OnJoyInput == null)
                return;

            for (int i = 0; i < _joyCount; i++)
            {
                try
                {
                    var joystick = _joyDevice[i];
                    if (joystick == null) continue;

                    joystick.Poll();
                    var state = joystick.GetCurrentState();

                    if (state != null)
                        OnJoyInput(this, new JoyEventArgs(i, state));
                }
                catch
                {
                    try
                    {
                        _joyDevice[i]?.Unacquire();
                        _joyDevice[i]?.Acquire();
                    }
                    catch { }
                }
            }
        }

        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Dispose();
            _timer = null;

            for (int i = 0; i < _joyCount; i++)
            {
                _joyDevice[i]?.Unacquire();
                _joyDevice[i]?.Dispose();
                _joyDevice[i] = null;
            }

            _directInput?.Dispose();
        }
    }

    public class JoyEventArgs : EventArgs
    {
        public int JoyId { get; }
        public JoystickState JoyState { get; }

        public JoyEventArgs(int joyId, JoystickState joyState)
        {
            JoyId = joyId;
            JoyState = joyState;
        }
    }
}
