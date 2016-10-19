#pragma strict
var target:GameObject;
function Start () {

}

function Update () {
    transform.LookAt(Vector3(target.transform.position.x,target.transform.position.y,target.transform.position.z));
}