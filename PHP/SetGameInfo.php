<?php
include_once "config.php";
$conn = mysqli_connect(DB_HOST, DB_USER, DB_PASSWORD, DB_NAME) or die("connection failed"); //DB 연결 시도
mysqli_query($conn, "Set Names UTF8"); //UTP8 설정

$userID = $_POST["userID"]; //User ID 입력
$userName = $_POST["userName"]; //User Name 입력
$data = $_POST["data"]; //Data 입력

$queryResult = mysqli_query($conn, "Select * From GameInfo Where userID = '" . $userID . "'"); //쿼리 결과 저장

if(mysqli_num_rows($queryResult) == 0) //결과가 존재하지 않으면
{
  mysqli_query($conn, "Insert Into GameInfo(userID) Values ('". $userID ."')"); //레코드 추가
}

$jsonArray = json_decode($data, JSON_UNESCAPED_UNICODE); //JSON을 배열로 변환

mysqli_query($conn, "Update GameInfo Set userName='". $userName .
"', stage=". $jsonArray["stage"] .
", money=". $jsonArray["money"] .
", profit=". $jsonArray["profit"] .
", `kill`=". $jsonArray["kill"] .

", fistSelected=". (int)$jsonArray["fistSelected"] .
", fistBuying=". (int)$jsonArray["fistBuying"] .
", fistAttackLevel=". $jsonArray["fistAttackLevel"] .
", fistUltLevel=". $jsonArray["fistUltLevel"] .

", swordSelected=". (int)$jsonArray["swordSelected"] .
", swordBuying=".(int)$jsonArray["swordBuying"] .
", swordAttackLevel=". $jsonArray["swordAttackLevel"] .
", swordUltLevel=". $jsonArray["swordUltLevel"] .

", gunSelected=". (int)$jsonArray["gunSelected"] .
", gunBuying=". (int)$jsonArray["gunBuying"] .
", gunAttackLevel=". $jsonArray["gunAttackLevel"] .
", gunUltLevel=". $jsonArray["gunUltLevel"] .

", sniperSelected=". (int)$jsonArray["sniperSelected"] .
", sniperBuying=". (int)$jsonArray["sniperBuying"] .
", sniperAttackLevel=". $jsonArray["sniperAttackLevel"] .
", sniperUltLevel=". $jsonArray["sniperUltLevel"] .

", bazookaSelected=". (int)$jsonArray["bazookaSelected"] .
", bazookaBuying=". (int)$jsonArray["bazookaBuying"] .
", bazookaAttackLevel=". $jsonArray["bazookaAttackLevel"] .
", bazookaUltLevel=". $jsonArray["bazookaUltLevel"] .

", wizardSelected=". (int)$jsonArray["wizardSelected"] .
", wizardBuying=". (int)$jsonArray["wizardBuying"] .
", wizardAttackLevel=". $jsonArray["wizardAttackLevel"] .
", wizardUltLevel=". $jsonArray["wizardUltLevel"] .

", maxHPLevel=". $jsonArray["maxHPLevel"] .
", recoveryHPLevel=". $jsonArray["recoveryHPLevel"] .
", defenseLevel=". $jsonArray["defenseLevel"] .
", moveSpeedLevel=". $jsonArray["moveSpeedLevel"] .
", moneyAmountLevel=". $jsonArray["moneyAmountLevel"] .
", moneyProbabilityLevel=". $jsonArray["moneyProbabilityLevel"] .
", criticalDamageLevel=". $jsonArray["criticalDamageLevel"] .
", criticalProbabilityLevel=". $jsonArray["criticalProbabilityLevel"] .

" Where userID='". $userID ."'"); //레코드 업데이트

mysqli_close($conn); //연결 해제
?>
