using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NTPackage.Functions{
    public class NTBehaviour : MonoBehaviour
    {
        public bool funnyCheck;
        protected virtual void Reset()
        {
            this.LoadComponents();
            this.ResetValues();
        }

        protected virtual void Awake()
        {
            //For Override
        }

        protected virtual void Start()
        {
            //For Overide
        }

        protected virtual void Update()
        {
            //For Overide
        }

        protected virtual void FixedUpdate()
        {
            //For Overide
        }

        protected virtual void OnDisable()
        {
            //For Overide
        }

        protected virtual void OnEnable()
        {
            // this.LoadComponents();
            //For Overide
        }

        public virtual void  LoadComponents()
        {
            //For Overide
        }

        public virtual void  ResetValues()
        {
            //For Overide
        }
    }
}
