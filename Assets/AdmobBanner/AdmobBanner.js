#pragma strict
var publisherId : String = "7313245864178620";
var refreshRate : float = 30.0;
var testDevice : String = "test_device_code_here";
function Start () {
    AdBannerObserver.Initialize(publisherId, testDevice, refreshRate);
}
