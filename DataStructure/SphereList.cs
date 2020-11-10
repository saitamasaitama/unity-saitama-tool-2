using System;
using System.Collections;
using System.Collections.Generic;

//球
class SphereList<T>:List<SphereListItem<T>>{
   public SphereList(float radius):base(){
    
   }
   private int[] index;

}

class SphereListItem<T>{
  public T value;
  public SphereList<T> owner;


  public void Move(float x,float y){

  }
}
