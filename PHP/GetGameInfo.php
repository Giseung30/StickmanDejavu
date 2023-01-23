<?php
include_once "config.php";
$conn = mysqli_connect(DB_HOST, DB_USER, DB_PASSWORD, DB_NAME) or die("connection failed"); //DB 연결 시도
mysqli_query($conn, "Set Names UTF8"); //UTP8 설정

$userID = $_POST["userID"]; //User ID 입력

$queryResult = mysqli_query($conn, "Select * From GameInfo Where userID = '" . $userID . "'"); //쿼리 결과 저장

if(mysqli_num_rows($queryResult) > 0) //결과가 존재하면
{
  $queryArray = mysqli_fetch_array($queryResult); //쿼리 결과를 배열로 변환

  $jsonArray = array(
    "stage"=>(int)$queryArray["stage"],
    "money"=>(int)$queryArray["money"],
    "profit"=>(int)$queryArray["profit"],
    "kill"=>(int)$queryArray["kill"],

    "fistSelected"=>(bool)$queryArray["fistSelected"],
    "fistBuying"=>(bool)$queryArray["fistBuying"],
    "fistAttackLevel"=>(int)$queryArray["fistAttackLevel"],
    "fistUltLevel"=>(int)$queryArray["fistUltLevel"],

    "swordSelected"=>(bool)$queryArray["swordSelected"],
    "swordBuying"=>(bool)$queryArray["swordBuying"],
    "swordAttackLevel"=>(int)$queryArray["swordAttackLevel"],
    "swordUltLevel"=>(int)$queryArray["swordUltLevel"],

    "gunSelected"=>(bool)$queryArray["gunSelected"],
    "gunBuying"=>(bool)$queryArray["gunBuying"],
    "gunAttackLevel"=>(int)$queryArray["gunAttackLevel"],
    "gunUltLevel"=>(int)$queryArray["gunUltLevel"],

    "sniperSelected"=>(bool)$queryArray["sniperSelected"],
    "sniperBuying"=>(bool)$queryArray["sniperBuying"],
    "sniperAttackLevel"=>(int)$queryArray["sniperAttackLevel"],
    "sniperUltLevel"=>(int)$queryArray["sniperUltLevel"],

    "bazookaSelected"=>(bool)$queryArray["bazookaSelected"],
    "bazookaBuying"=>(bool)$queryArray["bazookaBuying"],
    "bazookaAttackLevel"=>(int)$queryArray["bazookaAttackLevel"],
    "bazookaUltLevel"=>(int)$queryArray["bazookaUltLevel"],

    "wizardSelected"=>(bool)$queryArray["wizardSelected"],
    "wizardBuying"=>(bool)$queryArray["wizardBuying"],
    "wizardAttackLevel"=>(int)$queryArray["wizardAttackLevel"],
    "wizardUltLevel"=>(int)$queryArray["wizardUltLevel"],

    "maxHPLevel"=>(int)$queryArray["maxHPLevel"],
    "recoveryHPLevel"=>(int)$queryArray["recoveryHPLevel"],
    "defenseLevel"=>(int)$queryArray["defenseLevel"],
    "moveSpeedLevel"=>(int)$queryArray["moveSpeedLevel"],
    "moneyAmountLevel"=>(int)$queryArray["moneyAmountLevel"],
    "moneyProbabilityLevel"=>(int)$queryArray["moneyProbabilityLevel"],
    "criticalDamageLevel"=>(int)$queryArray["criticalDamageLevel"],
    "criticalProbabilityLevel"=>(int)$queryArray["criticalProbabilityLevel"]
  ); //결과 배열

  echo json_encode($jsonArray, JSON_UNESCAPED_UNICODE); //결과 배열을 JSON으로 변환
}
else //결과가 존재하지 않으면
{
  echo "";
}

mysqli_close($conn); //연결 해제
?>
