using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Puzzle : MonoBehaviour
{
   public UnityEvent PuzzleIsEmpty;
   private int _count;

   void Start() {
      _count = 0;
     
     //You can setup the trigger here in script, or in the editor, doesn't really matter\
   }

   void OnTriggerEnter(Collider other) {
      ++_count;
      Debug.Log("isEmpty: " + isEmpty()+_count);
   }

   void OnTriggerExit(Collider other) {
      --_count;
      Debug.Log("isEmpty: " + isEmpty()+_count);
   }

   bool isEmpty() {
      PuzzleIsEmpty?.Invoke();
      return _count == 0;
   }
}