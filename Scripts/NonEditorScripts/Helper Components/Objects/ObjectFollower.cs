﻿using UnityEngine;
using CXUtils.CXMath;

public class ObjectFollower : MonoBehaviour
{
    #region Enums
    /// <summary> Option flags for the object to follow the position </summary>
    public enum ObjectFollowPositionOptions
    { None, All, HasOffsetOnly, HasLerpOnly }

    /// <summary> Option flags for the object to follow the rotation </summary>
    public enum ObjectFollowRotationOptions
    { None, HasLerp, NoLerp }

    /// <summary> Option flags for the update mode for the follower </summary>
    public enum ObjectUpdateOptions
    { Update, FixedUpdate, LateUpdate }

    #endregion

    #region Vars and enums

    [Header("Configuration")]
    public Transform transformTo;
    public ObjectFollowPositionOptions objectFollowPositionOptions = ObjectFollowPositionOptions.All;
    public ObjectFollowRotationOptions objectFollowRotationOptions = ObjectFollowRotationOptions.None;
    public ObjectUpdateOptions objectUpdateOptions = ObjectUpdateOptions.LateUpdate;


    [Range(0f, 100f), Tooltip("The lerp speed of the follower")]
    public float MovingSpeed = 2f;
    [Range(0f, 100f), Tooltip("The rotation speed of the follower")]
    public float RotationSpeed = 2f;

    public Vector3 offSet = Vector3.zero;
    #endregion

    #region UnityMethods

    private void Update()
    {
        if (objectUpdateOptions == ObjectUpdateOptions.Update)
            FollowObject();
    }

    private void FixedUpdate()
    {
        if (objectUpdateOptions == ObjectUpdateOptions.FixedUpdate)
            FollowObject();
    }

    private void LateUpdate()
    {
        if (objectUpdateOptions == ObjectUpdateOptions.LateUpdate)
            FollowObject();
    }

    #endregion

    #region MainMethods
    public void FollowObject()
    {
        FollowMove();
        FollowRotation();
    }
    public void FollowMove()
    {
        Vector3 newPos = transformTo.position;

        //check the has off set (if has then add)
        if (objectFollowPositionOptions == ObjectFollowPositionOptions.All || objectFollowPositionOptions == ObjectFollowPositionOptions.HasOffsetOnly)
            newPos += offSet;

        if (objectFollowPositionOptions == ObjectFollowPositionOptions.All || objectFollowPositionOptions == ObjectFollowPositionOptions.HasLerpOnly)
            newPos = Vector3.Lerp(transform.position, newPos, MathFunc.Map(MovingSpeed, 0, 100, 0, 1));


        //then just set it
        transform.position = newPos;

    }
    public void FollowRotation()
    {
        Quaternion newRot = default;

        if (objectFollowRotationOptions != ObjectFollowRotationOptions.None)
        {
            newRot = transformTo.rotation;

            if (objectFollowRotationOptions == ObjectFollowRotationOptions.NoLerp)
            {
                //then nothing (because it's up there already)
            }

            if (objectFollowRotationOptions == ObjectFollowRotationOptions.HasLerp)
                newRot = Quaternion.Lerp(transform.rotation, newRot, MathFunc.Map(RotationSpeed, 0, 100, 0, 1));
        }

        if (newRot != default)
            transform.rotation = newRot;
    }
    #endregion

}
