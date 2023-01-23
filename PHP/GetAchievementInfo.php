<?php
include_once "config.php";
$conn = mysqli_connect(DB_HOST, DB_USER, DB_PASSWORD, DB_NAME) or die("connection failed"); //DB 연결 시도
mysqli_query($conn, "Set Names UTF8"); //UTP8 설정

$userID = $_POST["userID"]; //User ID 입력

$result = mysqli_query($conn, "Select * From AchievementInfo Where userID = '" . $userID . "'"); //쿼리 결과 저장

if(mysqli_num_rows($result) > 0) //결과가 존재하면
{
  $resultArray = mysqli_fetch_array($result); //결과를 배열로 변경
  echo $resultArray["data"]; //Data 반환
}
else //결과가 존재하지 않으면
{
  echo "";
}

mysqli_close($conn); //연결 해제
?>
