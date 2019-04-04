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

	$stmt = $conn->prepare("SELECT `username`, (SELECT tier FROM ships WHERE name like user_ship.shipName) as tier,`no_cannons`, `speed`, `strength`, `turn_speed`, `singleCannonDmg`, `shipName` FROM `user_ship` WHERE username like ? ORDER BY tier ASC;");
	$stmt->bind_param("s", $existinginfo["username"]);
	$stmt->execute() or die("8: Ship check query failed");
	$result = $stmt->get_result();
	$stmt->close();

	echo "0_".$existinginfo["username"]."_".$existinginfo["no_ships"]."_".$existinginfo["gold"]."_".$existinginfo["rum"]."_".$existinginfo["wood"]."_".$existinginfo["pearl"]."_".$existinginfo["experience"]."_".$existinginfo["level"]."_".$existinginfo["no_victory"]."_".$existinginfo["no_lose"]."|";
	
	if($result->num_rows < 1){
		echo "9: User don't have ships";
		exit();
	}

	while ($row = $result->fetch_assoc()) {
			echo $row["shipName"]."_".$row["tier"]."_".$row["no_cannons"]."_".$row["speed"]."_".$row["strength"]."_".$row["turn_speed"]."_".$row["singleCannonDmg"]."|";
	}
	
?>