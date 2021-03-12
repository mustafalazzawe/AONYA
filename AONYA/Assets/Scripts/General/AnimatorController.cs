using UnityEngine;

public class AnimatorController : MonoBehaviour {
    [Header("Animator Controller params")]
    [SerializeField] private Animator anim;

    // set a float param
    public void SetAnimParameter(string floatName, float floatValue) {
        anim.SetFloat(floatName, floatValue);
    }

    // set a bool param
    public void SetAnimParameter(string boolName, bool boolValue) {
        anim.SetBool(boolName, boolValue);
    }

    // get a float param
    public float GetAnimFloat(string floatName) {
        return anim.GetFloat(floatName);
    }


    // get a bool param
    public bool GetAnimBool(string boolName) {
        return anim.GetBool(boolName);
    }
}
