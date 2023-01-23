<?php
include_once "config.php";
$conn = mysqli_connect(DB_HOST, DB_USER, DB_PASSWORD, DB_NAME) or die("connection failed"); //DB 연결 시도
mysqli_query($conn, "Set Names UTF8"); //UTP8 설정

$userID = $_POST["userID"]; //User ID 입력
$userName = $_POST["userName"]; //User Name 입력
$productID = $_POST["productID"]; //Product ID 입력

mysqli_query($conn, "Insert Into PurchaseInfo(userID, userName, productID) Values ('". $userID ."', '". $userName ."', '". $productID . "')"); //레코드 추가

mysqli_close($conn); //연결 해제
?>
