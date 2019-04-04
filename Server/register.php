<?php
	require ('inc_conn.php');
	
	$username = mysqli_real_escape_string($conn, $_POST["name"]);
	$email = mysqli_real_escape_string($conn, $_POST["email"]);
	$password = mysqli_real_escape_string($conn, $_POST["password"]);

	$stmt = $conn->prepare("SELECT username FROM users WHERE username=?;");
	$stmt->bind_param("s", $username);
	$stmt->execute() or die("2: Name check query failed");
	
	$rezultat = $stmt->get_result();

	if($rezultat->num_rows > 0){
		echo "3: Name already exists";
		exit();
	}
	
	$stmt = $conn->prepare("INSERT INTO users(username, email, password) VALUES (?,?,?)");
	$passwordHASH = password_hash($password, PASSWORD_DEFAULT);
	$stmt->bind_param("sss", $username, $email, $passwordHASH);
	$stmt->execute() or die("4: Insert player query failed");

	$stmt = $conn->prepare("INSERT INTO `user_ship`(`username`, `shipName`) VALUES (?,'Brig')");
	$stmt->bind_param("s", $username);
	$stmt->execute() or die("8: Insert first ship query failed");
	$stmt->close();

	echo "0";
?>