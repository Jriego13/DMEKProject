extends MarginContainer

const first_scene = preload("res://FirstScene.tscn")

onready var selector_one = $CenterContainer/VBoxContainer/CenterContainer2/VBoxContainer/CenterContainer/HBoxContainer/Selector
onready var selector_two = $CenterContainer/VBoxContainer/CenterContainer2/VBoxContainer/CenterContainer2/HBoxContainer/Selector
onready var selector_three = $CenterContainer/VBoxContainer/CenterContainer2/VBoxContainer/CenterContainer3/HBoxContainer/Selector
onready var selector_four = $CenterContainer/VBoxContainer/CenterContainer2/VBoxContainer/CenterContainer4/HBoxContainer/Selector

var current_selection = 0

func _ready():
	# on load method, first thing that gets called 
	set_current_selection(0)
	

func _process(delta):
	# process input 
	if (Input.is_action_just_pressed("ui_down") and current_selection < 3):
		current_selection += 1
		set_current_selection(current_selection)
	elif (Input.is_action_just_pressed("ui_up") and current_selection > 0 ):
		current_selection -= 1
		set_current_selection(current_selection)
	elif (Input.is_action_just_pressed("ui_accept")):
		handle_selection(current_selection)

func handle_selection(current_selection):
	if(current_selection == 0):
		get_parent().add_child(first_scene.instance())
		queue_free()
		# look into queue_free more
	elif(current_selection == 1):
		print("Add options later!")
	elif (current_selection == 2):
		print("Add information page later!")
	elif (current_selection == 3):
		get_tree().quit()
		

func set_current_selection(_current_selection):
	# logic for making selection button appear
	selector_one.text = ""
	selector_two.text = ""
	selector_three.text = ""
	selector_four.text = ""

	if(_current_selection == 0):
		selector_one.text = ">"
	elif (_current_selection == 1):
		selector_two.text = ">"
	elif(_current_selection == 2):
		selector_three.text = ">"
	elif (_current_selection == 3):
		selector_four.text = ">"
