using UnityEngine;
/*using UnityEngine.Localization.Settings;
using UnityEngine.Localization;*/

namespace Ata.Utils
{
    public static class Utilities
    {
        /// <summary>
        /// Clamps an angle between specified minimum and maximum limits, 
        /// normalizing it to the range [-360, 360] before clamping.
        /// </summary>
        /// <param name="lfAngle">The angle in degrees to clamp.</param>
        /// <param name="lfMin">The minimum allowed angle.</param>
        /// <param name="lfMax">The maximum allowed angle.</param>
        /// <returns>
        /// The clamped angle within the specified range.
        /// </returns>
        public static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }


        /// <summary>
        /// Normalizes a value within a specified range to a value between 0 and 1.
        /// </summary>
        /// <param name="value">The value to normalize.</param>
        /// <param name="minValue">The minimum value of the range.</param>
        /// <param name="maxValue">The maximum value of the range.</param>
        /// <returns>
        /// The normalized value clamped between 0 and 1.
        /// </returns>
        public static float NormalizeValue(float value, float minValue, float maxValue)
        {
            return Mathf.Clamp01((value - minValue) / (maxValue - minValue));
        }


        /// <summary>
        /// Rotates a direction vector around a specified axis by a given angle in degrees.
        /// </summary>
        /// <param name="direction">The original direction vector to rotate.</param>
        /// <param name="axis">The axis around which to rotate the vector (will be normalized).</param>
        /// <param name="angle">The angle in degrees to rotate the vector.</param>
        /// <returns>
        /// The rotated direction vector.
        /// </returns>
        public static Vector3 RotateDirectionVector(Vector3 direction, Vector3 axis, float angle)
        {
            return Quaternion.Euler(axis.normalized * angle) * direction;
        }


        /// <summary>
        /// Calculates the unsigned angle in degrees between two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>
        /// The angle in degrees between <paramref name="vector1"/> and <paramref name="vector2"/>.
        /// The result is always between 0 and 180 degrees.
        /// </returns>
        public static float AngleBetweenTwoVectors(Vector3 vector1, Vector3 vector2)
        {
            return Vector3.Angle(vector1, vector2);
        }


        /// <summary>
        /// Calculates the signed angle between two vectors, measured around a specified axis.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="alongAxis">
        /// The axis around which to measure the signed angle (typically <see cref="Vector3.up"/> for horizontal angles).
        /// </param>
        /// <returns>
        /// The signed angle in degrees between <paramref name="a"/> and <paramref name="b"/>, 
        /// positive or negative depending on their relative orientation around <paramref name="alongAxis"/>.
        /// </returns>
        public static float SignedAngleBetweenTwoVectors(Vector3 a, Vector3 b, Vector3 alongAxis)
        {
            return Vector3.SignedAngle(a, b, alongAxis);
        }


        /// <summary>
        /// Calculates the midpoint between two points in 3D space.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>
        /// A <see cref="Vector3"/> representing the point exactly in the middle of <paramref name="a"/> and <paramref name="b"/>.
        /// </returns>
        public static Vector3 MiddleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return (a + b) / 2f;
        }


        /// <summary>
        /// Calculates a world position by applying a local offset relative to the given transform.
        /// </summary>
        /// <param name="_transform">The transform to use as the reference point.</param>
        /// <param name="_offset">
        /// The local space offset as a Vector3 (X = right/left, Y = up/down, Z = forward/backward relative to the transform).
        /// </param>
        /// <returns>
        /// A world position corresponding to the transform's position plus the specified local offset.
        /// </returns>
        public static Vector3 GetRelativePositionWithOffset(Transform _transform, Vector3 _offset)
        {
            return _transform.TransformPoint(_offset);
        }


        /// <summary>
        /// Calculates a world position by applying a local offset relative to the given transform.
        /// </summary>
        /// <param name="_transform">The transform to use as the reference point.</param>
        /// <param name="_x">The local offset along the X axis (right/left relative to the transform).</param>
        /// <param name="_y">The local offset along the Y axis (up/down relative to the transform).</param>
        /// <param name="_z">The local offset along the Z axis (forward/backward relative to the transform).</param>
        /// <returns>
        /// A world position corresponding to the transform's position plus the specified local offset.
        /// </returns>
        public static Vector3 GetRelativePositionWithOffset(Transform _transform, float _x, float _y, float _z)
        {
            return _transform.TransformPoint(new(_x, _y, _z));
        }


        /// <summary>
        /// Changes the value of a specific axis (X, Y, or Z) in a given Vector3.
        /// </summary>
        /// <param name="_originalVector">The original Vector3 value.</param>
        /// <param name="_replacingValue">The new value to set for the selected axis.</param>
        /// <param name="_axisToChange">
        /// The axis to modify (use Vector3.right for X, Vector3.up for Y, or Vector3.forward for Z).
        /// </param>
        /// <returns>
        /// A new Vector3 with the specified axis replaced by the given value.
        /// </returns>
        public static Vector3 ChangeAxisValue(Vector3 _originalVector, float _replacingValue, Vector3 _axisToChange)
        {
            if (_axisToChange == Vector3.right)
                return new (_replacingValue, _originalVector.y, _originalVector.z);
            else if (_axisToChange == Vector3.up)
                return new (_originalVector.x, _replacingValue, _originalVector.z);
            else if (_axisToChange == Vector3.forward)
                return new (_originalVector.x, _originalVector.y, _replacingValue);

            // If axis is invalid, just return the original vector
            Debug.LogError("Axis is invalid");
            return _originalVector;
        }


        /// <summary>
        /// Calculates the direction vector from <paramref name="from"/> to <paramref name="to"/>.
        /// Optionally normalizes the result (default is true).
        /// </summary>
        public static Vector3 Direction(Vector3 from, Vector3 to, bool normalize = true)
        {
            Vector3 direction = to - from;
            if (normalize) direction.Normalize();
            return direction;
        }


        /// <summary>
        /// Calculates the horizontal (XZ plane) direction vector from <paramref name="from"/> to <paramref name="to"/>.
        /// Ignores vertical (Y axis) difference. Optionally normalizes the result (default is true).
        /// </summary>
        /// <param name="from">The starting point.</param>
        /// <param name="to">The target point.</param>
        /// <param name="normalize">Whether to normalize the direction vector.</param>
        /// <returns>The horizontal direction vector from <paramref name="from"/> to <paramref name="to"/>.</returns>
        public static Vector3 DirectionXZ(Vector3 from, Vector3 to, bool normalize = true)
        {
            Vector3 direction = to - from;
            direction.y = 0;
            if (normalize) direction.Normalize();
            return direction;
        }


        /// <summary>
        /// Plays the specified audio clip using the given audio source.
        /// Sets the clip on the source and starts playback.
        /// </summary>
        /// <param name="source">The AudioSource to play the clip on.</param>
        /// <param name="clip">The AudioClip to be played.</param>
        public static void PlayAudio(AudioSource source, AudioClip clip, bool dontTouchIfAlreadyPlaying = false)
        {
            if (source == null) return;

            if (source.clip == clip && source.isPlaying && dontTouchIfAlreadyPlaying) return;

            source.clip = clip;

            if (clip != null) source.Play();
        }


        /// <summary>
        /// Plays the specified audio clip using the given audio source.
        /// Sets the clip on the source and starts playback.
        /// </summary>
        /// <param name="source">The AudioSource to play the clip on.</param>
        /// <param name="clip">The AudioClip to be played.</param>
        public static void PlayAudio(AudioSource source, AudioClip clip, float pitch, bool dontTouchIfAlreadyPlaying = false)
        {
            if (source == null) return;

            if (source.clip == clip && source.isPlaying && dontTouchIfAlreadyPlaying) return;

            source.clip = clip;

            source.pitch = pitch;

            if (clip != null) source.Play();
        }


        /// <summary>
        /// Searches the available locales and returns the one matching the specified locale code.
        /// </summary>
        /// <param name="code">The locale code to search for (e.g., "en", "fr").</param>
        /// <returns>
        /// The <see cref="Locale"/> with the matching code, or <c>null</c> if no matching locale is found.
        /// </returns>
        /*public static Locale GetLocaleByCode(string code)
        {
            foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
            {
                if (locale.Identifier.Code == code)
                {
                    return locale;
                }
            }

            return null;
        }*/


        /// <summary>
        /// Rotates a transform to smoothly look at a target on the XZ plane (ignoring vertical difference).
        /// </summary>
        /// <param name="_tr">The transform to rotate.</param>
        /// <param name="_target">The target transform to look at.</param>
        /// <param name="_rotationSpeed">
        /// The speed factor for smoothing the rotation. Higher values rotate faster.
        /// </param>
        public static void TransformLookAtTargetXZ(Transform _tr, Transform _target, float _rotationSpeed)
        {
            _tr.rotation = Quaternion.LookRotation(
                Vector3.Lerp(
                _tr.forward,
                DirectionXZ(_tr.position, _target.position),
                Time.deltaTime * _rotationSpeed
                )
            );
        }


        /// <summary>
        /// Smoothly interpolates a float value towards a target value over time.
        /// </summary>
        /// <param name="current">The current value to be smoothed.</param>
        /// <param name="target">The desired target value to reach.</param>
        /// <param name="velocity">
        /// The current velocity of the value. This is modified by the function so it must be passed by reference.
        /// </param>
        /// <param name="smoothTime">
        /// The time it takes to reach the target. Smaller values result in faster smoothing.
        /// </param>
        /// <returns>
        /// The new smoothed float value closer to the target.
        /// </returns>
        public static float SmoothFloat(float current, float target, ref float velocity, float smoothTime)
        {
            return Mathf.SmoothDamp(current, target, ref velocity, smoothTime);
        }


        /// <summary>
        /// Smoothly interpolates a Vector3 value towards a target Vector3 over time.
        /// </summary>
        /// <param name="current">The current position to be smoothed.</param>
        /// <param name="target">The desired target position to reach.</param>
        /// <param name="velocity">
        /// The current velocity of the Vector3. This is modified by the function so it must be passed by reference.
        /// </param>
        /// <param name="smoothTime">
        /// The time it takes to reach the target. Smaller values result in faster smoothing.
        /// </param>
        /// <returns>
        /// The new smoothed Vector3 value closer to the target.
        /// </returns>
        public static Vector3 SmoothVector3(Vector3 current, Vector3 target, ref Vector3 velocity, float smoothTime)
        {
            return Vector3.SmoothDamp(current, target, ref velocity, smoothTime);
        }


        /// <summary>
        /// Smoothly interpolates a Quaternion rotation towards a target rotation over time.
        /// </summary>
        /// <param name="current">The current rotation.</param>
        /// <param name="target">The target rotation.</param>
        /// <param name="velocity">
        /// A Vector3 representing the current angular velocity in degrees per axis.
        /// This is modified by the function so it must be passed by reference.
        /// </param>
        /// <param name="smoothTime">
        /// The time it takes to reach the target. Smaller values result in faster smoothing.
        /// </param>
        /// <returns>
        /// The new smoothed Quaternion closer to the target rotation.
        /// </returns>
        public static Quaternion SmoothQuaternion(Quaternion current, Quaternion target, ref Vector3 velocity, float smoothTime)
        {
            // Decompose into Euler angles
            Vector3 currentEuler = current.eulerAngles;
            Vector3 targetEuler = target.eulerAngles;

            // Smooth each axis separately
            float x = Mathf.SmoothDampAngle(currentEuler.x, targetEuler.x, ref velocity.x, smoothTime);
            float y = Mathf.SmoothDampAngle(currentEuler.y, targetEuler.y, ref velocity.y, smoothTime);
            float z = Mathf.SmoothDampAngle(currentEuler.z, targetEuler.z, ref velocity.z, smoothTime);

            return Quaternion.Euler(x, y, z);
        }


        /// <summary>
        /// Projects a vector onto the plane defined by a given surface normal.
        /// This ensures the resulting vector lies tangent to the surface,
        /// removing any component in the direction of the normal.
        /// </summary>
        /// <param name="current">
        /// The input vector to be projected (e.g., a movement or direction vector).
        /// </param>
        /// <param name="groundNormal">
        /// The normal vector of the plane to project onto. 
        /// If the normal is nearly zero, the original vector is returned.
        /// </param>
        /// <returns>
        /// A vector lying on the plane defined by <paramref name="groundNormal"/>,
        /// tangent to the surface.
        /// </returns>
        public static Vector3 ProjectOnPlane(Vector3 current, Vector3 groundNormal)
        {
            if (groundNormal.sqrMagnitude < 1e-6f) return current;

            return Vector3.ProjectOnPlane(current, groundNormal);
        }


        /// <summary>
        /// Returns either 1 or -1 at random with equal probability.
        /// </summary>
        /// <returns>1 or -1, chosen randomly.</returns>
        public static int Get1OrMinus1()
        {
            return Random.Range(0, 2) * 2 - 1;
        }


        /// <summary>
        /// Generates a random uppercase alphabetic string of the specified length.
        /// </summary>
        /// <param name="length">The number of characters in the generated string.</param>
        /// <returns>A random string consisting of uppercase letters A–Z.</returns>
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[Random.Range(0, chars.Length)];
            }
            return new string(result);
        }
    }
}