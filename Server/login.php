<?php
	require ('inc_conn.php');
	
	$username = mysqli_real_escape_string($conn, $_POST["name"]);
	$password = mysqli_real_escape_string($conn, $_POST["password"]); // Kriptiranje lozinke

    $stmt = $conn->prepare("SELECT username, password, no_ships, gold, rum, wood, pearl, experience, level, no_victory, no_lose FROM users WHERE username=?;");
	$stmt->bind_param("s", $username);
	$stmt->execute() or die("2: Name check query failed");
	
	$rezultat = $stmt->get_result();

	$existinginfo = $rezultat->fetch_assoc();
	$stmt->close();

	if($rezultat->num_rows  != 1){
		echo "5: Either no user with name or more than one";
		exit();
	}else if(!password_verify($password, $existinginfo["password"])){
		echo "6: Password is not correct";
		exit();
	}

	$stmt = $conn->prepare("SELECT username, password, no_ships, gold, rum, wood, pearl, experience, level, no_victory, no_lose FROM users WHERE username=?;");
	
    echo "0\t".$existinginfo["username"]."\t".$existinginfo["no_ships"]."\t".$existinginfo["gold"]."\t".$existinginfo["rum"]."\t".$existinginfo["wood"]."\t".$existinginfo["pearl"]."\t".$existinginfo["experience"]."\t".$existinginfo["level"]."\t".$existinginfo["no_victory"]."\t".$existinginfo["no_lose"];
?>