<?php
include_once "config.php";
$conn = mysqli_connect(DB_HOST, DB_USER, DB_PASSWORD, DB_NAME) or die("connection failed"); //DB 연결 시도
mysqli_query($conn, "Set Names UTF8"); //UTP8 설정

$userID = $_POST["userID"]; //User ID 입력
$userName = $_POST["userName"]; //User Name 입력
$data = $_POST["data"]; //Data 입력

$result = mysqli_query($conn, "Select * From AchievementInfo Where userID = '" . $userID . "'"); //쿼리 결과 저장

if(mysqli_num_rows($result) == 0) //결과가 존재하지 않으면
{
  mysqli_query($conn, "Insert Into AchievementInfo(userID) Values ('". $userID ."')"); //레코드 추가
}

mysqli_query($conn, "Update AchievementInfo Set userName='". $userName ."', data='". $data ."' Where userID='". $userID ."'"); //레코드 업데이트

mysqli_close($conn); //연결 해제
?>
