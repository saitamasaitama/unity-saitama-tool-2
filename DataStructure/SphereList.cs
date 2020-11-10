using System;
using System.Collections;
using System.Collections.Generic;

//球
class SphereList<T>:List<SphereListItem<T>>{
   public SphereList<T>(float radius):base<T>(){
    
   }
   private int[] index;

}

class SphereListItem<T>{
  public T value;
  public SphereList<T> owner;


  public Move(float x,float y){

  }
}
