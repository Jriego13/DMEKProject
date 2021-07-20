extends MarginContainer

const first_scene = preload("res://FirstScene.tscn")
const main_scene = preload("res://SampleEye.tscn")
const information_scene = preload("res://TestingInfo.tscn");

onready var Start = $CenterContainer/VBoxContainer/CenterContainer2/VBoxContainer/Start
onready var Option = $CenterContainer/VBoxContainer/CenterContainer2/VBoxContainer/Option
onready var Information = $CenterContainer/VBoxContainer/CenterContainer2/VBoxContainer/Information
onready var Exit= $CenterContainer/VBoxContainer/CenterContainer2/VBoxContainer/Exit



onready var Start_selector = Start.get_node("HBoxContainer/Selector")
onready var Option_selector = Option.get_node("HBoxContainer/Selector")
onready var Information_selector = Information.get_node("HBoxContainer/Selector")
onready var Exit_selector = Exit.get_node("HBoxContainer/Selector")

var current_selection = 0

func _ready():
	
	Start.get_node("HBoxContainer/OptionName").text = "Start"
	Option.get_node("HBoxContainer/OptionName").text = "Option"
	Information.get_node("HBoxContainer/OptionName").text = "Information"
	Exit.get_node("HBoxContainer/OptionName").text = "Exit"
	
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
		get_parent().add_child(main_scene.instance())
		queue_free()
		# look into queue_free more
	elif(current_selection == 1):
		print("Add options later!")
	elif (current_selection == 2):
		get_parent().add_child(information_scene.instance())
		queue_free()
	elif (current_selection == 3):
		get_tree().quit()
		

func set_current_selection(_current_selection):
	# logic for making selection button appear
	Start_selector.text = ""
	Option_selector.text = ""
	Information_selector.text = ""
	Exit_selector.text = ""

	if(_current_selection == 0):
		Start_selector.text = ">"
	elif (_current_selection == 1):
		Option_selector.text = ">"
	elif(_current_selection == 2):
		Information_selector.text = ">"
	elif (_current_selection == 3):
		Exit_selector.text = ">"
