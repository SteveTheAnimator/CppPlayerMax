using CppPlayerMax.Tools;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CppPlayerMax
{
    public class RigWrapper : MonoBehaviour
    {

        public PhotonView photonView;
        public VRRig rig;
        public AudioSource voiceAudio;
        public string userId;
        public int volume;
        Traverse rigTraverse;
        public bool invalid;

        float refreshInterval = 5f, lastRefresh;

        void Awake() => Refresh();

        void FixedUpdate()
        {
            if (Time.time - lastRefresh > refreshInterval)
            {
                Refresh();
                lastRefresh = Time.time;
            }
        }

        public void Refresh()
        {
            try
            {
                rig = GetComponent<VRRig>();
                rigTraverse = Traverse.Create(rig);
                photonView = rigTraverse.Field("photonView").GetValue<PhotonView>();
                voiceAudio = rigTraverse.Field("voiceAudio").GetValue<AudioSource>();
                userId = photonView.Owner.UserId;

                // Set spatial blend directly or use a default value if not found
                float spatialBlend = 0.5f; // You can adjust this value as needed
                voiceAudio.spatialBlend = spatialBlend;

                // Set max distance directly or use a default value if not found
                float maxDistance = 2f; // You can adjust this value as needed
                voiceAudio.maxDistance = maxDistance;

                invalid = false;
            }
            catch (Exception e)
            {
                invalid = true;
                //Logging.Exception(e);
            }
        }
    }
}
