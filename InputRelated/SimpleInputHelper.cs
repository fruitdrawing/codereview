using System.Collections.Generic;
using MyDomain.Utils.MyExtensionMethods;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace DefaultNamespace.MyScripts.InputRelated
{
    public class SimpleInputHelper : MonoBehaviour
    {
        public bool Available;

        public ReactiveProperty<float> Speed = new(2);
        public ReactiveProperty<float> AdditinoalSpeed = new(0);
        public Vector3 CurrentInput;
        public Rigidbody2D Rigidbody2D;

        public GenericDictionary<KeyCode, bool> KeyPressing = new GenericDictionary<KeyCode, bool>()
        {
            new KeyValuePair<KeyCode, bool>(KeyCode.LeftArrow, false),
            new KeyValuePair<KeyCode, bool>(KeyCode.RightArrow, false),
            new KeyValuePair<KeyCode, bool>(KeyCode.UpArrow, false),
            new KeyValuePair<KeyCode, bool>(KeyCode.DownArrow, false)
        };
        
        private bool RightArrowPressing;
        private bool LeftArrowPressing;
        private bool UpArrowPressing;
        private bool DownArrowPressing;
        private void Awake()
        {
            foreach (var VARIABLE in KeyPressing)
            {
                SetupKeyCode(VARIABLE.Key);
            }

            this.UpdateAsObservable().Where(_ => Input.GetKey(KeyCode.LeftShift))
                .Subscribe(_ =>
                {
                    
                    AdditinoalSpeed.Value = 4;
                }).AddTo(this);
            this.UpdateAsObservable().Where(_ => !Input.GetKey(KeyCode.LeftShift))
                .Subscribe(_ =>
                {

                    AdditinoalSpeed.Value = 0;
                }).AddTo(this);
            
            this.UpdateAsObservable().Where(_=>Available == true).Subscribe(_ =>
            {
                
                CurrentInput = new Vector3(0,0,0);
                foreach (var VARIABLE in KeyPressing)
                {
                    if (VARIABLE.Key == KeyCode.LeftArrow && VARIABLE.Value == true)
                    {
                        CurrentInput += Vector3.left;
                    }
                    else if((VARIABLE.Key == KeyCode.RightArrow  && VARIABLE.Value == true))
                    {
                        CurrentInput += Vector3.right;

                    }   
                    else if((VARIABLE.Key == KeyCode.UpArrow  && VARIABLE.Value == true))
                    {
                        CurrentInput += Vector3.up;
 
                    }   
                    else if((VARIABLE.Key == KeyCode.DownArrow  && VARIABLE.Value == true))
                    {
                        CurrentInput += Vector3.down;
                    }
                }

            }).AddTo(this);
            


            this.FixedUpdateAsObservable().Where(_=>Available == true).Subscribe(_ =>
            {
                Rigidbody2D.MovePosition((CurrentInput *Time.deltaTime*(Speed.Value+AdditinoalSpeed.Value)+ this.transform.position));
            }).AddTo(this);
        }

        private void SetupKeyCode(KeyCode keyCode)
        {
            this.UpdateAsObservable().Where(_=>Input.GetKey(keyCode)).Subscribe(_ =>
            {
                KeyPressing[keyCode] = true;
            }).AddTo(this);
            
            this.UpdateAsObservable().Where(_=>Input.GetKey(keyCode) == false).Subscribe(_ =>
            {
                KeyPressing[keyCode] = false;
            }).AddTo(this);
        }
    }
}