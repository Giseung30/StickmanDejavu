<?php
include_once "config.php";
$conn = mysqli_connect(DB_HOST, DB_USER, DB_PASSWORD, DB_NAME) or die("connection failed"); //DB 연결 시도
mysqli_query($conn, "Set Names UTF8"); //UTP8 설정

$userID = $_POST["userID"]; //User ID 입력
$userName = $_POST["userName"]; //User Name 입력
$stage = $_POST["stage"]; //Stage 입력
$currentKill = $_POST["currentKill"]; //CurrentKill 입력
$currentMoney = $_POST["currentMoney"]; //CurrentMoney 입력
$clearReward = $_POST["clearReward"]; //ClearReward 입력
$totalProfit = $_POST["totalProfit"]; //TotalProfit 입력
$isADReward = $_POST["isADReward"]; //IsADReward 입력

mysqli_query($conn, "Insert Into ResultInfo(userID, userName, stage, currentKill, currentMoney, clearReward, totalProfit, isADReward) Values ('"
. $userID ."', '". $userName ."', ". $stage . ", ". $currentKill .", ". $currentMoney .", ". $clearReward .", ". $totalProfit .", ". $isADReward .")"); //레코드 추가

mysqli_close($conn); //연결 해제
?>
