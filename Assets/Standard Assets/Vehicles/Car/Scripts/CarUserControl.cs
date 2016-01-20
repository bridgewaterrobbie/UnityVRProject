using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
		public float maxRollAngle = 80;
		public float maxPitchAngle = 80;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
			float roll = CrossPlatformInputManager.GetAxis("Horizontal");
			float pitch = CrossPlatformInputManager.GetAxis("Vertical");
			bool airBrakes = CrossPlatformInputManager.GetButton("Fire1");
			float throttle = airBrakes ? -1 : 1;


#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);

			m_Car.Move(roll, pitch, 0, throttle, airBrakes);

#else
			AdjustInputForMobileControls(ref roll, ref pitch, ref throttle);

            m_Car.Move(h, v, v, 0f);
			m_Car.Move(roll, pitch, 0, throttle, airBrakes);
#endif
			if (Input.GetKeyDown ("x")) {
				m_Car.switchMode ();
			}
		
		}

		private void AdjustInputForMobileControls(ref float roll, ref float pitch, ref float throttle)
		{
			// because mobile tilt is used for roll and pitch, we help out by
			// assuming that a centered level device means the user
			// wants to fly straight and level!

			// this means on mobile, the input represents the *desired* roll angle of the aeroplane,
			// and the roll input is calculated to achieve that.
			// whereas on non-mobile, the input directly controls the roll of the aeroplane.

			float intendedRollAngle = roll*maxRollAngle*Mathf.Deg2Rad;
			float intendedPitchAngle = pitch*maxPitchAngle*Mathf.Deg2Rad;
			roll = Mathf.Clamp((intendedRollAngle - m_Car.RollAngle), -1, 1);
			pitch = Mathf.Clamp((intendedPitchAngle - m_Car.PitchAngle), -1, 1);

			// similarly, the throttle axis input is considered to be the desired absolute value, not a relative change to current throttle.
			float intendedThrottle = throttle*0.5f + 0.5f;
			throttle = Mathf.Clamp(intendedThrottle - m_Car.Throttle, -1, 1);
		}
    }
}
