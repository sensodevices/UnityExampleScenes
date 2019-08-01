using UnityEngine;

public class SensoHandExample : Senso.Hand {

    public Transform Palm;
    public Transform Wrist;

    public Transform[] thumbBones;
    public Transform[] indexBones;
    public Transform[] middleBones;
    public Transform[] thirdBones;
    public Transform[] littleBones;

    [SerializeField] GameObject[] Hands;
    [SerializeField] private Gestures[] Gesture;
    [HideInInspector]
    private SensoHandExample[] Hand;

    private Quaternion[][] fingerInitialRotations;

    public new void Start ()
	{
		base.Start();
        fingerInitialRotations = new Quaternion[5][];
        for (int i = 0; i < 5; i++)
        {
            Transform[] arr;
            switch (i)
            {
                case 1: arr = indexBones; break;
                case 2: arr = middleBones; break;
                case 3: arr = thirdBones; break;
                case 4: arr = littleBones; break;
                default: arr = thumbBones; break;
            }
            fingerInitialRotations[i] = new Quaternion[arr.Length];
            for (int j = 0; j < arr.Length; ++j)
                fingerInitialRotations[i][j] = arr[j].localRotation;
        }

        if (Hands == null)
        {
            Hands = GameObject.FindGameObjectsWithTag("Hand Container");
            for (int i = 0; i != 2; i++)
            {
                Gesture[i] = Hands[i].GetComponent<Gestures>();
                Hand[i] = Hands[i].GetComponent<SensoHandExample>();

            }

        }
    }


    public override void SetSensoPose (Senso.HandData aData)
	{
        //Wrist.localRotation = aData.WristRotation;
        Palm.localRotation = /*(Quaternion.Inverse(wq) */ aData.PalmRotation;
        Palm.localPosition = aData.PalmPosition;
        

		//Fingers
        if (aData.AdvancedThumb)
        {
            //Quaternion thumbQ = new Quaternion(aData.ThumbQuaternion.y / 3.0f, aData.ThumbQuaternion.x, -aData.ThumbQuaternion.z, aData.ThumbQuaternion.w);
            Quaternion thumbQ = new Quaternion(aData.ThumbQuaternion.z, aData.ThumbQuaternion.y, -aData.ThumbQuaternion.x / 3.0f, aData.ThumbQuaternion.w);
            thumbBones[0].localRotation = fingerInitialRotations[0][0] * thumbQ;
            thumbBones[1].localRotation = fingerInitialRotations[0][1] * Quaternion.Euler(0.0f, 0.0f, -aData.ThumbQuaternion.x / 3.0f);
            thumbBones[2].localRotation = fingerInitialRotations[0][2] * Quaternion.Euler(0.0f, 0.0f, aData.ThumbBend * Mathf.Rad2Deg);
        }
        else // old method
		      setFingerBones(ref thumbBones, aData.ThumbAngles, Senso.EFingerType.Thumb);
		setFingerBones(ref indexBones, aData.IndexAngles, Senso.EFingerType.Index);
		setFingerBones(ref middleBones, aData.MiddleAngles, Senso.EFingerType.Middle);
		setFingerBones(ref thirdBones, aData.ThirdAngles, Senso.EFingerType.Third);
		setFingerBones(ref littleBones, aData.LittleAngles, Senso.EFingerType.Little);
	}

	private void setFingerBones(ref Transform[] bones, Vector2 angles, Senso.EFingerType fingerType)
	{
		if (fingerType == Senso.EFingerType.Thumb) setThumbBones(ref bones, ref angles);
		else {
            var fingerIdx = (int)fingerType;
            var vec = new Vector3(-angles.y, 0, angles.x);
            if (vec.x > 0.0f) {
                vec.z += fingerInitialRotations[fingerIdx][0].eulerAngles.z;
                bones[0].localEulerAngles = vec;

            } else {
                var v = vec;
                v.x /= 3.0f;
                bones[0].localRotation = fingerInitialRotations[fingerIdx][0];
                bones[0].Rotate(v);
                vec.z = 0.0f;

                vec.x /= 1.5f;
				for (int j = 1; j < bones.Length; ++j) {
                    bones[j].localRotation = fingerInitialRotations[fingerIdx][j];
                    bones[j].Rotate(vec);
				}
			}
		}
	}

	private static void setThumbBones(ref Transform[] bones, ref Vector2 angles) {
		bones[0].localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
		float t = angles.x;
		angles.x = -angles.y;
		angles.y = t;

		angles.y += 30.0f;
		bones[0].Rotate(angles);
	}
}
