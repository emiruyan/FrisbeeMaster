using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrisbeeController : MonoBehaviour
{
   [SerializeField] private float initialVelocity;
   [SerializeField] private float _angle;
   [SerializeField] private LineRenderer line;
   [SerializeField] private float step;

   private void Update()
   {
      float angle = _angle * Mathf.Deg2Rad;
      DrawPath(initialVelocity, angle,step);
      if (Input.GetMouseButtonDown(0))
      {
         
         StopAllCoroutines();
         StartCoroutine(CoroutineMovement(initialVelocity, angle));
      } 
   }

   private  void DrawPath(float v0, float angle, float step)
   {
      step = Mathf.Max(0.01f, step);
      float totalTime = 10;
      line.positionCount = (int) (totalTime / step) + 2;
      int count = 0;
      for (float i = 0; i < totalTime; i += step)
      {
         float x = v0 * i * Mathf.Cos(angle);
         float z = v0 * i * Mathf.Sin(angle) - 0.5f * -Physics.gravity.z * Mathf.Pow(i, 2);
      }
      
      float xFinal = v0 * totalTime * Mathf.Cos(angle);//daha sonra target konulacak
      float zFinal = v0 * totalTime * Mathf.Sin(angle) - 0.5f * -Physics.gravity.z * Mathf.Pow(totalTime, 2);
      line.SetPosition(count, new Vector3(xFinal,zFinal, 0));
   }
   
   IEnumerator CoroutineMovement(float v0, float angle)
   {
      float t = 0;
      while (t < 100)
      {
         float x = v0 * t * Mathf.Cos(angle);
         float z = v0 * t * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.z * Mathf.Pow(t, 2);
         transform.position = new Vector3(x, 0, z);
         t += Time.deltaTime;
         yield return null;
      }
   }
}
