Imports System.IO
Imports System.Text

Public Class SourceQcFile

#Region "Methods"

	Public Sub WriteFile(ByVal outputPathFileName As String, ByVal aSourceEngineModel As SourceModel)
		Me.theSourceEngineModel = aSourceEngineModel

		Me.WriteQcFile(outputPathFileName)

		'Me.WriteBatFile(pathFileName)
		'Me.WriteMainFile(pathFileName)
		'Me.WriteModelNameFile(pathFileName, "hlmv")
		'Me.WriteModelNameFile(pathFileName, "l4d2")
	End Sub

#End Region

#Region "Private Methods"

	Private Sub WriteQcFile(ByVal outputPathFileName As String)
		Me.theOutputFileStream = File.CreateText(outputPathFileName)

		Me.WriteHeaderComment()

		Me.WriteModelNameCommand()

		Me.WriteStaticPropCommand()
		Me.WriteConstDirectionalLightCommand()

		If Me.theSourceEngineModel.theMdlFileHeader.theModelCommandIsUsed Then
			Me.WriteModelCommand()
			Me.WriteBodyGroupCommand(1)
		Else
			Me.WriteBodyGroupCommand(0)
		End If
		Me.WriteLodCommand()
		Me.WriteNoForcedFadeCommand()
		Me.WriteForcePhonemeCrossfadeCommand()

		Me.WritePoseParameterCommand()

		Me.WriteAmbientBoostCommand()
		Me.WriteOpaqueCommand()
		Me.WriteObsoleteCommand()
		Me.WriteCdMaterialsCommand()
		Me.WriteTextureGroupCommand()
		If TheApp.Settings.DecompileQcFileExtraInfoIsChecked Then
			Me.WriteTextureFileNameComments()
		End If

		Me.WriteAttachmentCommand()

		Me.WriteSurfacePropCommand()
		Me.WriteJointSurfacePropCommand()
		Me.WriteContentsCommand()
		Me.WriteJointContentsCommand()

		Me.WriteEyePositionCommand()
		If TheApp.Settings.DecompileQcFileExtraInfoIsChecked Then
			Me.WriteIllumPositionCommand()
		End If

		If TheApp.Settings.DecompileQcFileExtraInfoIsChecked Then
			Me.WriteBBoxCommand()
			Me.WriteCBoxCommand()
		End If
		If theSourceEngineModel.theMdlFileHeader.theHitboxSets IsNot Nothing Then
			Me.WriteHboxRelatedCommands()
		End If

		Me.WriteControllerCommand()
		Me.WriteScreenAlignCommand()

		Me.WriteSectionFramesCommand()
		Me.WriteAnimationAndDeclareAnimationCommand()
		Me.WriteSequenceAndDeclareSequenceCommand()
		Me.WriteIncludeModelCommand()
		Me.WriteIkChainCommand()
		Me.WriteIkAutoPlayLockCommand()
		Me.WriteBoneSaveFrameCommand()

		Me.WriteCollisionModelOrCollisionJointsCommand()
		Me.WriteCollisionTextCommand()

		Me.WriteProceduralBonesCommand()
		Me.WriteBoneMergeCommand()
		Me.WriteJiggleBoneCommand()

		Me.WriteKeyValues(theSourceEngineModel.theMdlFileHeader.theKeyValuesText, "$keyvalues")

		theOutputFileStream.Flush()
		theOutputFileStream.Close()
	End Sub

	'Private Sub WriteBatFile(ByVal pathFileName As String)
	'	Dim outputPathFileName As String

	'	outputPathFileName = Path.Combine(Path.GetDirectoryName(pathFileName), "compile_model.bat")
	'	Me.theOutputFileStream = File.CreateText(outputPathFileName)

	'	Dim line As String = ""

	'	line = "@REM "
	'	line += TheApp.GetHeaderComment()
	'	theOutputFileStream.WriteLine(line)

	'	line = "@ECHO OFF"
	'	theOutputFileStream.WriteLine(line)

	'	theOutputFileStream.WriteLine()
	'	line = "SET ModelName="
	'	line += "decompiled"
	'	theOutputFileStream.WriteLine(line)

	'	theOutputFileStream.WriteLine()
	'	line = "SET Left4Dead2PathRootFolder=C:\Program Files (x86)\Steam\SteamApps\common\left 4 dead 2\"
	'	theOutputFileStream.WriteLine(line)
	'	line = "SET StudiomdlPathName=%Left4Dead2PathRootFolder%bin\studiomdl.exe"
	'	theOutputFileStream.WriteLine(line)
	'	line = "SET Left4Dead2PathSubFolder=%Left4Dead2PathRootFolder%left4dead2"
	'	theOutputFileStream.WriteLine(line)
	'	line = "SET StudiomdlParams=-game ""%Left4Dead2PathSubFolder%"" -nop4 -verbose"
	'	theOutputFileStream.WriteLine(line)

	'	theOutputFileStream.WriteLine()
	'	line = "TITLE Compile %ModelName%"
	'	theOutputFileStream.WriteLine(line)
	'	line = "PROMPT $G"
	'	theOutputFileStream.WriteLine(line)
	'	line = "CLS"
	'	theOutputFileStream.WriteLine(line)

	'	theOutputFileStream.WriteLine()
	'	line = ":show_menu"
	'	theOutputFileStream.WriteLine(line)
	'	line = "ECHO."
	'	theOutputFileStream.WriteLine(line)
	'	line = "ECHO [1] Compile model for HLMV"
	'	theOutputFileStream.WriteLine(line)
	'	line = "ECHO [2] Compile model for L4D2"
	'	theOutputFileStream.WriteLine(line)
	'	line = "ECHO [X] Exit"
	'	theOutputFileStream.WriteLine(line)
	'	line = "ECHO ------"
	'	theOutputFileStream.WriteLine(line)

	'	theOutputFileStream.WriteLine()
	'	line = "	REM These lines do not work for XP. Does not require pressing ""Enter"" key to complete the input."
	'	theOutputFileStream.WriteLine(line)
	'	line = "CHOICE /C 12X "
	'	theOutputFileStream.WriteLine(line)
	'	line = "IF errorlevel 3 goto:eof"
	'	theOutputFileStream.WriteLine(line)
	'	line = "IF errorlevel 2 goto l4d2"
	'	theOutputFileStream.WriteLine(line)
	'	line = "IF errorlevel 1 goto hlmv"
	'	theOutputFileStream.WriteLine(line)

	'	theOutputFileStream.WriteLine()
	'	line = "	REM Use these lines for XP, Vista, or Win 7. Requires pressing ""Enter"" key to complete the input."
	'	theOutputFileStream.WriteLine(line)
	'	line = "	REM Set /P menuitem=[1,2,X]?"
	'	theOutputFileStream.WriteLine(line)
	'	line = "	REM If ""%menuitem%""==""1"" goto world_hlmv"
	'	theOutputFileStream.WriteLine(line)
	'	line = "	REM If ""%menuitem%""==""2"" goto world_l4d2"
	'	theOutputFileStream.WriteLine(line)
	'	line = "	REM If /I ""%menuitem%""==""x"" goto:eof"
	'	theOutputFileStream.WriteLine(line)

	'	theOutputFileStream.WriteLine()
	'	line = ":hlmv"
	'	theOutputFileStream.WriteLine(line)
	'	line = "SET TargetApp=HLMV"
	'	theOutputFileStream.WriteLine(line)
	'	line = "goto compile"
	'	theOutputFileStream.WriteLine(line)

	'	theOutputFileStream.WriteLine()
	'	line = ":l4d2"
	'	theOutputFileStream.WriteLine(line)
	'	line = "SET TargetApp=L4D2"
	'	theOutputFileStream.WriteLine(line)
	'	line = "goto compile"
	'	theOutputFileStream.WriteLine(line)

	'	theOutputFileStream.WriteLine()
	'	line = ":compile"
	'	theOutputFileStream.WriteLine(line)
	'	line = "CLS"
	'	theOutputFileStream.WriteLine(line)
	'	line = "ECHO."
	'	theOutputFileStream.WriteLine(line)
	'	line = "ECHO Compiling model for %TargetApp%..."
	'	theOutputFileStream.WriteLine(line)
	'	line = "SET FileName=%ModelName%_%TargetApp%"
	'	theOutputFileStream.WriteLine(line)
	'	line = """%StudiomdlPathName%"" %StudiomdlParams% .\%FileName%.qc > .\%FileName%.log"
	'	theOutputFileStream.WriteLine(line)
	'	line = "ECHO ...Done."
	'	theOutputFileStream.WriteLine(line)
	'	line = "ECHO ================================================================================"
	'	theOutputFileStream.WriteLine(line)
	'	line = "goto show_menu"
	'	theOutputFileStream.WriteLine(line)

	'	theOutputFileStream.Flush()
	'	theOutputFileStream.Close()
	'End Sub
	
	'Private Sub WriteMainFile(ByVal pathFileName As String)
	'	Dim outputPathFileName As String

	'	'outputPathFileName = Path.ChangeExtension(pathFileName, ".qc")
	'	outputPathFileName = Path.Combine(Path.GetDirectoryName(pathFileName), "decompiled.qci")
	'	Me.theOutputFileStream = File.CreateText(outputPathFileName)

	'	Me.WriteHeaderComment()

	'	'Me.WriteModelNameCommand()

	'	Me.WriteStaticPropCommand()
	'	Me.WriteConstDirectionalLightCommand()

	'	If Me.theSourceEngineModel.theMdlFileHeader.theModelCommandIsUsed Then
	'		Me.WriteModelCommand()
	'		Me.WriteBodyGroupCommand(1)
	'	Else
	'		Me.WriteBodyGroupCommand(0)
	'	End If
	'	Me.WriteLodCommand()
	'	Me.WriteNoForcedFadeCommand()
	'	Me.WriteForcePhonemeCrossfadeCommand()

	'	Me.WritePoseParameterCommand()

	'	Me.WriteAmbientBoostCommand()
	'	Me.WriteOpaqueCommand()
	'	Me.WriteObsoleteCommand()
	'	Me.WriteCdMaterialsCommand()
	'	Me.WriteTextureGroupCommand()
	'	Me.WriteTextureFileNameComments()

	'	Me.WriteAttachmentCommand()

	'	Me.WriteSurfacePropCommand()
	'	Me.WriteJointSurfacePropCommand()
	'	Me.WriteContentsCommand()
	'	Me.WriteJointContentsCommand()

	'	Me.WriteEyePositionCommand()
	'	Me.WriteIllumPositionCommand()

	'	Me.WriteBBoxCommand()
	'	Me.WriteCBoxCommand()
	'	Me.WriteHboxRelatedCommands()

	'	Me.WriteControllerCommand()
	'	Me.WriteScreenAlignCommand()

	'	Me.WriteAnimationAndDeclareAnimationCommand()
	'	Me.WriteSequenceAndDeclareSequenceCommand()
	'	Me.WriteIncludeModelCommand()
	'	Me.WriteIkChainCommand()
	'	Me.WriteIkAutoPlayLockCommand()
	'	Me.WriteBoneSaveFrameCommand()

	'	Me.WriteCollisionModelOrCollisionJointsCommand()
	'	Me.WriteCollisionTextCommand()

	'	Me.WriteProceduralBonesCommand()
	'	Me.WriteBoneMergeCommand()
	'	Me.WriteJiggleBoneCommand()

	'	Me.WriteKeyValues(theSourceEngineModel.theMdlFileHeader.theKeyValuesText, "$keyvalues")

	'	theOutputFileStream.Flush()
	'	theOutputFileStream.Close()
	'End Sub

	'Private Sub WriteModelNameFile(ByVal pathFileName As String, ByVal type As String)
	'	Dim outputPathFileName As String
	'	Dim fileName As String
	'	Dim modelName As String
	'	Dim modelFileName As String

	'	fileName = "decompiled_"
	'	fileName += type
	'	fileName += ".qc"
	'	outputPathFileName = Path.Combine(FileManager.GetPath(pathFileName), fileName)
	'	Me.theOutputFileStream = File.CreateText(outputPathFileName)

	'	Me.WriteHeaderComment()

	'	If type = "hlmv" Then
	'		modelName = CStr(theSourceEngineModel.theMdlFileHeader.name).Trim(Chr(0))
	'		modelFileName = Path.GetFileNameWithoutExtension(modelName)

	'		'modelName = TheApp.GetPath(modelName)
	'		'modelName += "/custom/"
	'		'modelName += "decompiled"
	'		'modelName += ".mdl"
	'		'------
	'		modelName = "custom/"
	'		modelName += modelFileName
	'		modelName += ".mdl"

	'		Me.WriteModelNameCommand(modelName)
	'	Else
	'		Me.WriteModelNameCommand(CStr(theSourceEngineModel.theMdlFileHeader.name).Trim(Chr(0)))
	'	End If
	'	Me.WriteIncludeMainQcLine()

	'	theOutputFileStream.Flush()
	'	theOutputFileStream.Close()
	'End Sub

	Private Sub WriteModelNameCommand()
		Dim line As String = ""
		'Dim modelPath As String
		Dim modelPathFileName As String

		'modelPath = FileManager.GetPath(CStr(theSourceEngineModel.theMdlFileHeader.name).Trim(Chr(0)))
		'modelPathFileName = Path.Combine(modelPath, theSourceEngineModel.ModelName + ".mdl")
		modelPathFileName = CStr(theSourceEngineModel.theMdlFileHeader.name).Trim(Chr(0))

		theOutputFileStream.WriteLine()

		'$modelname "survivors/survivor_producer.mdl"
		'$modelname "custom/survivor_producer.mdl"
		line = "$modelname "
		line += """"
		line += modelPathFileName
		line += """"
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteIncludeMainQcLine()
		Dim line As String = ""

		theOutputFileStream.WriteLine()

		'$include "Rochelle_world.qci"
		line = "$include "
		line += """"
		line += "decompiled.qci"
		line += """"
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteHeaderComment()
		Dim line As String = ""

		line = "// "
		line += TheApp.GetHeaderComment()
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteStaticPropCommand()
		Dim line As String = ""

		'$staticprop
		If (theSourceEngineModel.theMdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
			theOutputFileStream.WriteLine()

			line = "$staticprop"
			theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteConstDirectionalLightCommand()
		Dim line As String = ""

		'$constantdirectionallight
		If (theSourceEngineModel.theMdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_CONSTANT_DIRECTIONAL_LIGHT_DOT) > 0 Then
			theOutputFileStream.WriteLine()

			line = "$constantdirectionallight "
			'FROM: studiomdl.cpp
			'g_constdirectionalightdot = (byte)( verify_atof(token) * 255.0f );
			line += CStr(Me.theSourceEngineModel.theMdlFileHeader.directionalLightDot / 255)
			theOutputFileStream.WriteLine(line)
		End If
	End Sub

	'Private Function GetModelPathFileName(ByVal aModel As SourceMdlModel) As String
	'	Dim pathFileName As String

	'	'NOTE: Use Path.GetFileName() to avoid writing relative-path file names: $model "TeenAngst" "../dmx/zoey_reference_wrinkle.dmx" {
	'	'NOTE: Avoid this example: 	replacemodel "../dmx/zoey_reference_wrinkle.dmx" "../dmx/zoey_reference_wrinkle.dmx_lod1"
	'	'line += CStr(theSourceEngineModel.theMdlFileData.theBodyParts(0).theModels(0).name).Trim(Chr(0))
	'	'line += Path.GetFileName(CStr(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0).name).Trim(Chr(0)))
	'	'NOTE: In general, do not add the ".smd" because the MDL file will store it if it was compiled with it.
	'	'line += ".smd"
	'	pathFileName = Path.GetFileName(CStr(aModel.name).Trim(Chr(0)))

	'	'NOTE: Add the ".smd" when ends with ".dmx" or else the qc file won't be able to compile.
	'	'If modelFileName.EndsWith(".dmx") Then
	'	'	modelFileName += ".smd"
	'	'End If
	'	'------
	'	If Path.GetExtension(pathFileName) <> ".smd" Then
	'		pathFileName = Path.ChangeExtension(pathFileName, ".smd")
	'	End If

	'	Return pathFileName
	'End Function

	Private Sub WriteModelCommand()
		Dim line As String = ""
		Dim referenceSmdFileName As String
		'Dim aBone As SourceMdlBone
		Dim eyeballNames As List(Of String)

		eyeballNames = New List(Of String)()

		'$model "producer" "producer_model_merged.dmx.smd" {
		'//-doesn't work     eyeball righteye ValveBiped.Bip01_Head1 -1.260 -0.086 64.594 eyeball_r 1.050  3.000 producer_head 0.530
		'//-doesn't work     eyeball lefteye ValveBiped.Bip01_Head1 1.260 -0.086 64.594 eyeball_l 1.050  -3.000 producer_head 0.530
		'     mouth 0 "mouth"  ValveBiped.Bip01_Head1 0.000 1.000 0.000
		'}
		If theSourceEngineModel.theMdlFileHeader.theBodyParts IsNot Nothing AndAlso theSourceEngineModel.theMdlFileHeader.theBodyParts.Count > 0 Then
			line = ""
			theOutputFileStream.WriteLine(line)

			'referenceSmdFileName = Me.GetModelPathFileName(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0))
			'referenceSmdFileName = theSourceEngineModel.GetLodSmdFileName(0)
			referenceSmdFileName = Me.theSourceEngineModel.GetBodyGroupSmdFileName(0, 0, 0)

			line = "$model "
			line += """"
			line += theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theName
			line += """ """
			line += referenceSmdFileName
			line += """"

			line += " {"
			theOutputFileStream.WriteLine(line)

			'NOTE: Must call WriteEyeballLines() before WriteEyelidLines(), because eyeballNames are created in first and sent to other.
			Me.WriteEyeballLines(eyeballNames)
			Me.WriteEyelidLines(eyeballNames)

			Me.WriteMouthLines()

			Me.WriteFlexLines()
			Me.WriteFlexControllerLines()
			Me.WriteFlexRuleLines()

			line = "}"
			theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteEyeballLines(ByRef eyeballNames As List(Of String))
		Dim line As String = ""
		Dim aBodyPart As SourceMdlBodyPart
		Dim aModel As SourceMdlModel
		Dim anEyeball As SourceMdlEyeball
		Dim eyeballTextureName As String
		Dim diameter As Double
		Dim angle As Double
		Dim irisScale As Double
		Dim poseToBone0 As SourceVector
		Dim poseToBone1 As SourceVector
		Dim poseToBone2 As SourceVector
		Dim poseToBone3 As SourceVector
		Dim eyeballPosition As SourceVector

		poseToBone0 = New SourceVector()
		poseToBone1 = New SourceVector()
		poseToBone2 = New SourceVector()
		poseToBone3 = New SourceVector()
		eyeballPosition = New SourceVector()

		Try
			'eyeball righteye ValveBiped.Bip01_Head1 -1.160 -3.350 62.600 teenangst_eyeball_r 1.000  3.000 zoey_color 0.630
			'eyeball lefteye ValveBiped.Bip01_Head1 1.160 -3.350 62.600 teenangst_eyeball_l 1.000  -3.000 zoey_color 0.630
			aBodyPart = theSourceEngineModel.theMdlFileHeader.theBodyParts(0)
			If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
				aModel = aBodyPart.theModels(0)
				If aModel.theEyeballs IsNot Nothing AndAlso aModel.theEyeballs.Count > 0 Then
					line = ""
					theOutputFileStream.WriteLine(line)

					For eyeballIndex As Integer = 0 To aModel.theEyeballs.Count - 1
						anEyeball = aModel.theEyeballs(eyeballIndex)

						'eyeballPosition.x = CSng(Math.Round(anEyeball.org.x, 3))
						'eyeballPosition.y = CSng(Math.Round(anEyeball.org.y, 3))
						'eyeballPosition.z = CSng(Math.Round(anEyeball.org.z, 3))
						'======
						'DONE: Transform vertices from Pose to Bone space, i.e. reverse these operations.
						'FROM: studiomdl.cpp
						'For boneToPose[]:
						'AngleMatrix( psource->rawanim[0][i].rot, m );
						'm[0][3] = psource->rawanim[0][i].pos[0];
						'm[1][3] = psource->rawanim[0][i].pos[1];
						'm[2][3] = psource->rawanim[0][i].pos[2];
						'// translate eyeball into bone space
						'VectorITransform( tmp, pmodel->source->boneToPose[eyeball->bone], eyeball->org );
						'------
						' WORKS!
						Dim aBone As SourceMdlBone
						aBone = Me.theSourceEngineModel.theMdlFileHeader.theBones(anEyeball.boneIndex)
						'AngleMatrix(aBone.rotationX, aBone.rotationY, aBone.rotationZ, poseToBone0, poseToBone1, poseToBone2)
						'poseToBone3.x = -aBone.positionX
						'poseToBone3.y = -aBone.positionY
						'poseToBone3.z = -aBone.positionZ
						'eyeballPosition = MathModule.VectorITransform(anEyeball.org, poseToBone0, poseToBone1, poseToBone2, poseToBone3)
						eyeballPosition = MathModule.VectorITransform(anEyeball.org, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)

						'FROM: studiomdl.cpp
						'eyeball->radius = verify_atof (token) / 2.0;
						diameter = anEyeball.radius * 2
						'FROM: studiomdl.cpp
						'eyeball->zoffset = tan( DEG2RAD( verify_atof (token) ) );
						angle = Math.Round(MathModule.RadiansToDegrees(Math.Atan(anEyeball.zOffset)), 6)
						'FROM: studiomdl.cpp
						'eyeball->iris_scale = 1.0 / verify_atof( token );
						irisScale = 1 / anEyeball.irisScale

						'NOTE: The mdl file does not store the eyeball name; studiomdl uses name once for checking eyelid info.
						'      So, just use an arbitrary name and guess which eyeball goes with which eyelid.
						'      Typically, there are only two eyeballs and right one has angle > 0 and left one has angle < 0.
						If eyeballIndex = 0 AndAlso angle > 0 Then
							eyeballNames.Add("eye_right")
						ElseIf eyeballIndex = 1 AndAlso angle < 0 Then
							eyeballNames.Add("eye_left")
						Else
							eyeballNames.Add("eye_" + eyeballIndex.ToString(TheApp.InternalNumberFormat))
						End If

						If anEyeball.theTextureIndex = -1 Then
							eyeballTextureName = "[unknown_texture]"
						Else
							eyeballTextureName = Path.GetFileName(theSourceEngineModel.theMdlFileHeader.theTextures(anEyeball.theTextureIndex).theName)
						End If

						line = vbTab
						line += "eyeball """
						line += eyeballNames(eyeballIndex)
						line += """ """
						line += theSourceEngineModel.theMdlFileHeader.theBones(anEyeball.boneIndex).theName
						line += """ "
						line += eyeballPosition.x.ToString("0.000000", TheApp.InternalNumberFormat)
						line += " "
						line += eyeballPosition.y.ToString("0.000000", TheApp.InternalNumberFormat)
						line += " "
						line += eyeballPosition.z.ToString("0.000000", TheApp.InternalNumberFormat)
						line += " """
						line += eyeballTextureName
						line += """ "
						line += diameter.ToString("0.######", TheApp.InternalNumberFormat)
						line += " "
						line += angle.ToString("0.######", TheApp.InternalNumberFormat)
						line += " "
						'Unused in later Source Engine versions.
						line += """iris_unused"""
						line += " "
						line += Math.Round(irisScale, 6).ToString("0.######", TheApp.InternalNumberFormat)
						theOutputFileStream.WriteLine(line)

						'NOTE: Used to write frame indexes for eyelid lines and prevent eyelid flexes from being written in flex list in qc file.
						theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.upperLidFlexDesc).theDescIsUsedByEyelid = True
						theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.lowerLidFlexDesc).theDescIsUsedByEyelid = True
					Next
				End If
			End If
		Catch
		End Try

		Me.CreateListOfEyelidFlexFrameIndexes()
	End Sub

	Private Sub CreateListOfEyelidFlexFrameIndexes()
		Dim aFlexFrame As FlexFrame

		theSourceEngineModel.theMdlFileHeader.theEyelidFlexFrameIndexes = New List(Of Integer)()
		For frameIndex As Integer = 1 To theSourceEngineModel.theMdlFileHeader.theFlexFrames.Count - 1
			aFlexFrame = theSourceEngineModel.theMdlFileHeader.theFlexFrames(frameIndex)
			If Not theSourceEngineModel.theMdlFileHeader.theEyelidFlexFrameIndexes.Contains(frameIndex) AndAlso Me.theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlexFrame.flexes(0).flexDescIndex).theDescIsUsedByEyelid Then
				theSourceEngineModel.theMdlFileHeader.theEyelidFlexFrameIndexes.Add(frameIndex)
			End If
		Next
	End Sub

	Private Sub WriteEyelidLines(ByVal eyeballNames As List(Of String))
		Dim line As String = ""
		Dim aBodyPart As SourceMdlBodyPart
		Dim aModel As SourceMdlModel
		Dim anEyeball As SourceMdlEyeball
		Dim frameIndex As Integer
		Dim eyelidName As String

		Try
			' Write eyelid options.
			'$definevariable expressions "zoeyp.vta"
			'eyelid  upper_right $expressions$ lowerer 1 -0.19 neutral 0 0.13 raiser 2 0.27 split 0.1 eyeball righteye
			'eyelid  lower_right $expressions$ lowerer 3 -0.32 neutral 0 -0.19 raiser 4 -0.02 split 0.1 eyeball righteye
			'eyelid  upper_left $expressions$ lowerer 1 -0.19 neutral 0 0.13 raiser 2 0.27 split -0.1 eyeball lefteye
			'eyelid  lower_left $expressions$ lowerer 3 -0.32 neutral 0 -0.19 raiser 4 -0.02 split -0.1 eyeball lefteye
			aBodyPart = theSourceEngineModel.theMdlFileHeader.theBodyParts(0)
			If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 AndAlso theSourceEngineModel.theMdlFileHeader.theEyelidFlexFrameIndexes.Count > 0 Then
				aModel = aBodyPart.theModels(0)
				If aModel.theEyeballs IsNot Nothing AndAlso aModel.theEyeballs.Count > 0 Then
					line = ""
					theOutputFileStream.WriteLine(line)

					frameIndex = 0
					For eyeballIndex As Integer = 0 To aModel.theEyeballs.Count - 1
						anEyeball = aModel.theEyeballs(eyeballIndex)

						If frameIndex + 3 >= theSourceEngineModel.theMdlFileHeader.theEyelidFlexFrameIndexes.Count Then
							frameIndex = 0
						End If
						eyelidName = theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.upperLidFlexDesc).theName

						line = vbTab
						line += "eyelid "
						line += eyelidName
						'line += " """
						'line += Path.GetFileNameWithoutExtension(CStr(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0).name).Trim(Chr(0)))
						'line += ".vta"" "
						line += " """
						line += theSourceEngineModel.GetVtaFileName()
						line += """ "
						line += "lowerer "
						'TODO: The frame indexes here and for raiser need correcting.
						'line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.upperFlexDesc(0)).theVtaFrameIndex.ToString()
						'TEST:
						'line += anEyeball.upperFlexDesc(0).ToString()
						'TEST:
						line += theSourceEngineModel.theMdlFileHeader.theEyelidFlexFrameIndexes(frameIndex).ToString(TheApp.InternalNumberFormat)
						frameIndex += 1
						line += " "
						line += anEyeball.upperTarget(0).ToString("0.##", TheApp.InternalNumberFormat)
						line += " "
						line += "neutral 0"
						'line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.upperFlexDesc(1)).theVtaFrameIndex.ToString()
						line += " "
						line += anEyeball.upperTarget(1).ToString("0.##", TheApp.InternalNumberFormat)
						line += " "
						line += "raiser "
						'line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.upperFlexDesc(2)).theVtaFrameIndex.ToString()
						'TEST:
						'line += anEyeball.upperFlexDesc(2).ToString()
						'TEST:
						line += theSourceEngineModel.theMdlFileHeader.theEyelidFlexFrameIndexes(frameIndex).ToString(TheApp.InternalNumberFormat)
						frameIndex += 1
						line += " "
						line += anEyeball.upperTarget(2).ToString("0.##", TheApp.InternalNumberFormat)
						line += " "
						line += "split "
						'TODO: simplify.cpp RemapVertexAnimations(); probably should call SourceMdlFile.GetSplit()?
						line += Me.GetSplitNumber(eyelidName)
						line += " eyeball """
						line += eyeballNames(eyeballIndex)
						line += """"
						theOutputFileStream.WriteLine(line)

						theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.upperLidFlexDesc).theDescIsUsedByFlex = True
						theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.upperFlexDesc(0)).theDescIsUsedByFlex = True
						theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.upperFlexDesc(1)).theDescIsUsedByFlex = True
						theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.upperFlexDesc(2)).theDescIsUsedByFlex = True

						eyelidName = theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.lowerLidFlexDesc).theName

						line = vbTab
						line += "eyelid "
						line += eyelidName
						'line += " """
						'line += Path.GetFileNameWithoutExtension(CStr(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0).name).Trim(Chr(0)))
						'line += ".vta"" "
						line += " """
						line += theSourceEngineModel.GetVtaFileName()
						line += """ "
						line += "lowerer "
						'line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.lowerFlexDesc(0)).theVtaFrameIndex.ToString()
						'TEST:
						'line += anEyeball.lowerFlexDesc(0).ToString()
						'TEST:
						line += theSourceEngineModel.theMdlFileHeader.theEyelidFlexFrameIndexes(frameIndex).ToString(TheApp.InternalNumberFormat)
						frameIndex += 1
						line += " "
						line += anEyeball.lowerTarget(0).ToString("0.##", TheApp.InternalNumberFormat)
						line += " "
						line += "neutral 0"
						'line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.lowerFlexDesc(1)).theVtaFrameIndex.ToString()
						line += " "
						line += anEyeball.lowerTarget(1).ToString("0.##", TheApp.InternalNumberFormat)
						line += " "
						line += "raiser "
						'line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.lowerFlexDesc(2)).theVtaFrameIndex.ToString()
						'TEST:
						'line += anEyeball.lowerFlexDesc(2).ToString()
						'TEST:
						line += theSourceEngineModel.theMdlFileHeader.theEyelidFlexFrameIndexes(frameIndex).ToString(TheApp.InternalNumberFormat)
						frameIndex += 1
						line += " "
						line += anEyeball.lowerTarget(2).ToString("0.##", TheApp.InternalNumberFormat)
						line += " "
						line += "split "
						'TODO: simplify.cpp RemapVertexAnimations(); probably should call SourceMdlFile.GetSplit()?
						line += Me.GetSplitNumber(eyelidName)
						line += " eyeball """
						line += eyeballNames(eyeballIndex)
						line += """"
						theOutputFileStream.WriteLine(line)

						theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.lowerLidFlexDesc).theDescIsUsedByFlex = True
						theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.lowerFlexDesc(0)).theDescIsUsedByFlex = True
						theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.lowerFlexDesc(1)).theDescIsUsedByFlex = True
						theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.lowerFlexDesc(2)).theDescIsUsedByFlex = True
					Next
				End If
			End If
		Catch
		End Try
	End Sub

	Private Function GetSplitNumber(ByVal eyelidName As String) As String
		If eyelidName.Contains("right") Then
			Return "1"
		ElseIf eyelidName.Contains("left") Then
			Return "-1"
		Else
			Return "0"
		End If
	End Function

	Private Sub WriteMouthLines()
		Dim line As String = ""
		Dim offsetX As Double
		Dim offsetY As Double
		Dim offsetZ As Double

		'NOTE: Writes out mouth line correctly for teenangst zoey.
		If theSourceEngineModel.theMdlFileHeader.theMouths IsNot Nothing AndAlso theSourceEngineModel.theMdlFileHeader.theMouths.Count > 0 Then
			line = ""
			theOutputFileStream.WriteLine(line)

			For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theMouths.Count - 1
				Dim aMouth As SourceMdlMouth
				aMouth = theSourceEngineModel.theMdlFileHeader.theMouths(i)
				offsetX = Math.Round(aMouth.forwardX, 3)
				offsetY = Math.Round(aMouth.forwardY, 3)
				offsetZ = Math.Round(aMouth.forwardZ, 3)

				line = vbTab
				line += "mouth "
				line += i.ToString(TheApp.InternalNumberFormat)
				line += " """
				line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aMouth.flexDescIndex).theName
				line += """ """
				line += theSourceEngineModel.theMdlFileHeader.theBones(aMouth.boneIndex).theName
				line += """ "
				line += offsetX.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += offsetY.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += offsetZ.ToString("0.######", TheApp.InternalNumberFormat)
				theOutputFileStream.WriteLine(line)

				theSourceEngineModel.theMdlFileHeader.theFlexDescs(aMouth.flexDescIndex).theDescIsUsedByFlex = True
			Next
		End If
	End Sub

	Private Sub WriteFlexLines()
		Dim line As String = ""

		' Write flexfile (contains flexDescs).
		If theSourceEngineModel.theMdlFileHeader.theFlexFrames IsNot Nothing AndAlso theSourceEngineModel.theMdlFileHeader.theFlexFrames.Count > 0 Then
			line = ""
			theOutputFileStream.WriteLine(line)

			line = vbTab
			line += "flexfile"
			'line += Path.GetFileNameWithoutExtension(CStr(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0).name).Trim(Chr(0)))
			'line += ".vta"""
			line += " """
			line += theSourceEngineModel.GetVtaFileName()
			line += """ "
			theOutputFileStream.WriteLine(line)

			line = vbTab
			line += "{"
			theOutputFileStream.WriteLine(line)

			'======
			line = vbTab
			line += vbTab
			line += "defaultflex frame 0"
			theOutputFileStream.WriteLine(line)

			'NOTE: Start at index 1 because defaultflex frame is at index 0.
			Dim aFlexFrame As FlexFrame
			For frameIndex As Integer = 1 To theSourceEngineModel.theMdlFileHeader.theFlexFrames.Count - 1
				aFlexFrame = theSourceEngineModel.theMdlFileHeader.theFlexFrames(frameIndex)
				line = vbTab
				line += vbTab
				If Me.theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlexFrame.flexes(0).flexDescIndex).theDescIsUsedByEyelid Then
					line += "// Already in eyelid lines: "
				End If
				If aFlexFrame.flexHasPartner Then
					line += "flexpair """
					line += aFlexFrame.flexName.Substring(0, aFlexFrame.flexName.Length - 1)
				Else
					line += "flex """
					line += aFlexFrame.flexName
				End If
				line += """"
				If aFlexFrame.flexHasPartner Then
					line += " "
					line += aFlexFrame.flexSplit.ToString("0.######", TheApp.InternalNumberFormat)
				End If
				line += " frame "
				line += CStr(frameIndex)
				theOutputFileStream.WriteLine(line)
			Next
			'======
			'Dim aBodyPart As SourceMdlBodyPart
			'Dim aModel As SourceMdlModel
			'Dim frameIndex As Integer
			'Dim flexDescHasBeenWritten As List(Of Integer)
			'Dim meshVertexIndexStart As Integer
			'frameIndex = 0
			'flexDescHasBeenWritten = New List(Of Integer)

			'line = vbTab
			'line += "defaultflex frame "
			'line += frameIndex.ToString()
			'theOutputFileStream.WriteLine(line)

			'For bodyPartIndex As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theBodyParts.Count - 1
			'	aBodyPart = theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex)

			'	If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
			'		For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
			'			aModel = aBodyPart.theModels(modelIndex)

			'			If aModel.theMeshes IsNot Nothing AndAlso aModel.theMeshes.Count > 0 Then
			'				For meshIndex As Integer = 0 To aModel.theMeshes.Count - 1
			'					Dim aMesh As SourceMdlMesh
			'					aMesh = aModel.theMeshes(meshIndex)

			'					meshVertexIndexStart = Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex).theModels(modelIndex).theMeshes(meshIndex).vertexIndexStart

			'					If aMesh.theFlexes IsNot Nothing AndAlso aMesh.theFlexes.Count > 0 Then
			'						For flexIndex As Integer = 0 To aMesh.theFlexes.Count - 1
			'							Dim aFlex As SourceMdlFlex
			'							aFlex = aMesh.theFlexes(flexIndex)

			'							If flexDescHasBeenWritten.Contains(aFlex.flexDescIndex) Then
			'								Continue For
			'							Else
			'								flexDescHasBeenWritten.Add(aFlex.flexDescIndex)
			'							End If

			'							line = vbTab
			'							Dim aFlexDescPartnerIndex As Integer
			'							'Dim aFlexPartner As SourceMdlFlex
			'							aFlexDescPartnerIndex = aMesh.theFlexes(flexIndex).flexDescPartnerIndex
			'							If aFlexDescPartnerIndex > 0 Then
			'								'aFlexPartner = theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlexDescPartnerIndex)
			'								If Not flexDescHasBeenWritten.Contains(aFlex.flexDescPartnerIndex) Then
			'									flexDescHasBeenWritten.Add(aFlex.flexDescPartnerIndex)
			'								End If
			'								line += "flexpair """
			'								Dim flexName As String
			'								flexName = theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theName
			'								line += flexName.Remove(flexName.Length - 1, 1)
			'								line += """"
			'								line += " "
			'								line += Me.GetSplit(aFlex, meshVertexIndexStart).ToString("0.######", TheApp.InternalNumberFormat)

			'								theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theDescIsUsedByFlex = True
			'								theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescPartnerIndex).theDescIsUsedByFlex = True
			'							Else
			'								line += "flex """
			'								line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theName
			'								line += """"

			'								theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theDescIsUsedByFlex = True
			'							End If
			'							line += " frame "
			'							'NOTE: Start at second frame because first frame is "basis" frame.
			'							frameIndex += 1
			'							line += frameIndex.ToString()
			'							'line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theVtaFrameIndex.ToString()
			'							theOutputFileStream.WriteLine(line)
			'						Next
			'					End If
			'				Next
			'			End If
			'		Next
			'	End If
			'Next

			line = vbTab
			line += "}"
			theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteFlexControllerLines()
		Dim line As String = ""

		'NOTE: Writes out flexcontrollers correctly for teenangst zoey.
		If theSourceEngineModel.theMdlFileHeader.theFlexControllers IsNot Nothing AndAlso theSourceEngineModel.theMdlFileHeader.theFlexControllers.Count > 0 Then
			Dim aFlexController As SourceMdlFlexController

			line = ""
			theOutputFileStream.WriteLine(line)

			For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theFlexControllers.Count - 1
				aFlexController = theSourceEngineModel.theMdlFileHeader.theFlexControllers(i)

				line = vbTab
				line += "flexcontroller "
				line += aFlexController.theType
				line += " "
				line += "range "
				line += aFlexController.min.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aFlexController.max.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aFlexController.theName
				theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	Private Sub WriteFlexRuleLines()
		Dim line As String = ""

		'NOTE: All flex rules are correct for teenangst zoey.
		If theSourceEngineModel.theMdlFileHeader.theFlexRules IsNot Nothing AndAlso theSourceEngineModel.theMdlFileHeader.theFlexRules.Count > 0 Then
			Dim aFlexRule As SourceMdlFlexRule

			line = ""
			theOutputFileStream.WriteLine(line)

			For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theFlexDescs.Count - 1
				Dim flexDesc As SourceMdlFlexDesc
				flexDesc = theSourceEngineModel.theMdlFileHeader.theFlexDescs(i)

				If Not flexDesc.theDescIsUsedByFlex AndAlso flexDesc.theDescIsUsedByFlexRule Then
					line = vbTab
					line += "localvar "
					line += flexDesc.theName
					theOutputFileStream.WriteLine(line)
				End If
			Next

			For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theFlexRules.Count - 1
				aFlexRule = theSourceEngineModel.theMdlFileHeader.theFlexRules(i)
				line = Me.GetFlexRule(aFlexRule)
				theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	Private Function GetFlexRule(ByVal aFlexRule As SourceMdlFlexRule) As String
		Dim flexRuleEquation As String
		flexRuleEquation = vbTab
		flexRuleEquation += "%"
		flexRuleEquation += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlexRule.flexIndex).theName
		flexRuleEquation += " = "
		If aFlexRule.theFlexOps IsNot Nothing AndAlso aFlexRule.theFlexOps.Count > 0 Then
			Dim aFlexOp As SourceMdlFlexOp

			' Convert to infix notation.

			Dim stack As Stack(Of IntermediateExpression) = New Stack(Of IntermediateExpression)()
			Dim rightExpr As String
			Dim leftExpr As String

			For i As Integer = 0 To aFlexRule.theFlexOps.Count - 1
				aFlexOp = aFlexRule.theFlexOps(i)
				If aFlexOp.op = SourceMdlFlexOp.STUDIO_CONST Then
					stack.Push(New IntermediateExpression(Math.Round(aFlexOp.value, 6).ToString("0.######", TheApp.InternalNumberFormat), 10))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_FETCH1 Then
					stack.Push(New IntermediateExpression(theSourceEngineModel.theMdlFileHeader.theFlexControllers(aFlexOp.index).theName, 10))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_FETCH2 Then
					stack.Push(New IntermediateExpression("%" + theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlexOp.index).theName, 10))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_ADD Then
					Dim rightIntermediate As IntermediateExpression = stack.Pop()
					Dim leftIntermediate As IntermediateExpression = stack.Pop()

					Dim newExpr As String = Convert.ToString(leftIntermediate.theExpression) + " + " + Convert.ToString(rightIntermediate.theExpression)
					stack.Push(New IntermediateExpression(newExpr, 1))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_SUB Then
					Dim rightIntermediate As IntermediateExpression = stack.Pop()
					Dim leftIntermediate As IntermediateExpression = stack.Pop()

					Dim newExpr As String = Convert.ToString(leftIntermediate.theExpression) + " - " + Convert.ToString(rightIntermediate.theExpression)
					stack.Push(New IntermediateExpression(newExpr, 1))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_MUL Then
					Dim rightIntermediate As IntermediateExpression = stack.Pop()
					If rightIntermediate.thePrecedence < 2 Then
						rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")"
					Else
						rightExpr = rightIntermediate.theExpression
					End If

					Dim leftIntermediate As IntermediateExpression = stack.Pop()
					If leftIntermediate.thePrecedence < 2 Then
						leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")"
					Else
						leftExpr = leftIntermediate.theExpression
					End If

					Dim newExpr As String = leftExpr + " * " + rightExpr
					stack.Push(New IntermediateExpression(newExpr, 2))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_DIV Then
					Dim rightIntermediate As IntermediateExpression = stack.Pop()
					If rightIntermediate.thePrecedence < 2 Then
						rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")"
					Else
						rightExpr = rightIntermediate.theExpression
					End If

					Dim leftIntermediate As IntermediateExpression = stack.Pop()
					If leftIntermediate.thePrecedence < 2 Then
						leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")"
					Else
						leftExpr = leftIntermediate.theExpression
					End If

					Dim newExpr As String = leftExpr + " / " + rightExpr
					stack.Push(New IntermediateExpression(newExpr, 2))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_NEG Then
					Dim rightIntermediate As IntermediateExpression = stack.Pop()

					Dim newExpr As String = "-" + rightIntermediate.theExpression
					stack.Push(New IntermediateExpression(newExpr, 10))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_MAX Then
					Dim rightIntermediate As IntermediateExpression = stack.Pop()
					If rightIntermediate.thePrecedence < 5 Then
						rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")"
					Else
						rightExpr = rightIntermediate.theExpression
					End If

					Dim leftIntermediate As IntermediateExpression = stack.Pop()
					If leftIntermediate.thePrecedence < 5 Then
						leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")"
					Else
						leftExpr = leftIntermediate.theExpression
					End If

					Dim newExpr As String = " max(" + leftExpr + ", " + rightExpr + ")"
					stack.Push(New IntermediateExpression(newExpr, 5))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_MIN Then
					Dim rightIntermediate As IntermediateExpression = stack.Pop()
					If rightIntermediate.thePrecedence < 5 Then
						rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")"
					Else
						rightExpr = rightIntermediate.theExpression
					End If

					Dim leftIntermediate As IntermediateExpression = stack.Pop()
					If leftIntermediate.thePrecedence < 5 Then
						leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")"
					Else
						leftExpr = leftIntermediate.theExpression
					End If

					Dim newExpr As String = " min(" + leftExpr + ", " + rightExpr + ")"
					stack.Push(New IntermediateExpression(newExpr, 5))
					'Else
					'	' Must be a number. Push it on the stack.
					'	stack.Push(New IntermediateExpression(token, ""))
				End If
			Next

			'' The loop above leaves the final expression on the top of the stack.
			If stack.Count > 0 Then
				flexRuleEquation += stack.Peek().theExpression
			Else
				flexRuleEquation = "// [Empty flex rule found and ignored.]"
			End If
		End If
		Return flexRuleEquation
	End Function

	Private Sub WriteLodCommand()
		Dim line As String = ""

		'NOTE: Data is from VTX file.
		'$lod 10
		' {
		'  replacemodel "producer_model_merged.dmx" "lod1_producer_model_merged.dmx"
		'}
		'$lod 15
		' {
		'  replacemodel "producer_model_merged.dmx" "lod2_producer_model_merged.dmx"
		'}
		'$lod 40
		' {
		'  replacemodel "producer_model_merged.dmx" "lod3_producer_model_merged.dmx"
		'}
		If theSourceEngineModel.theVtxFileHeader IsNot Nothing AndAlso Me.theSourceEngineModel.theMdlFileHeader.theBodyParts IsNot Nothing Then
			Dim referenceSmdFileName As String
			Dim lodSmdFileName As String

			If theSourceEngineModel.theVtxFileHeader.theVtxBodyParts Is Nothing Then
				Return
			End If
			If theSourceEngineModel.theVtxFileHeader.theVtxBodyParts(0).theVtxModels Is Nothing Then
				Return
			End If
			If theSourceEngineModel.theVtxFileHeader.theVtxBodyParts(0).theVtxModels(0).theVtxModelLods Is Nothing Then
				Return
			End If

			'referenceSmdFileName = Me.GetModelPathFileName(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0))
			''modelLodFileName = Path.GetFileName(CStr(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0).name).Trim(Chr(0)))
			'referenceSmdFileName = theSourceEngineModel.GetLodSmdFileName(0)
			referenceSmdFileName = theSourceEngineModel.GetBodyGroupSmdFileName(0, 0, 0)

			line = ""
			theOutputFileStream.WriteLine(line)

			'NOTE: Start loop at 1 to skip first LOD, which isn't needed for the $lod command.
			For lodIndex As Integer = 1 To theSourceEngineModel.theVtxFileHeader.lodCount - 1
				Dim switchPoint As Single
				'TODO: Need to check that each of these objects exist first before using them.
				If lodIndex >= theSourceEngineModel.theVtxFileHeader.theVtxBodyParts(0).theVtxModels(0).theVtxModelLods.Count Then
					Return
				End If

				switchPoint = theSourceEngineModel.theVtxFileHeader.theVtxBodyParts(0).theVtxModels(0).theVtxModelLods(lodIndex).switchPoint

				'lodSmdFileName = TheApp.GetModelLodFileName(TheApp.ModelName, lodIndex)
				'lodSmdFileName = theSourceEngineModel.GetLodSmdFileName(lodIndex)
				lodSmdFileName = theSourceEngineModel.GetBodyGroupSmdFileName(0, 0, lodIndex)

				line = ""
				If switchPoint = -1 Then
					'// Shadow lod reserves -1 as switch value
					'// which uniquely identifies a shadow lod
					'newLOD.switchValue = -1.0f;
					line += "$shadowlod"
				Else
					line += "$lod "
					line += switchPoint.ToString("0.######", TheApp.InternalNumberFormat)
				End If
				theOutputFileStream.WriteLine(line)

				line = "{"
				theOutputFileStream.WriteLine(line)

				line = vbTab
				line += "replacemodel "
				line += """"
				line += referenceSmdFileName
				line += """ """
				line += lodSmdFileName
				line += """"
				theOutputFileStream.WriteLine(line)

				line = "}"
				theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	Private Sub WriteNoForcedFadeCommand()
		Dim line As String = ""

		'$noforcedfade
		If (theSourceEngineModel.theMdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_NO_FORCED_FADE) > 0 Then
			theOutputFileStream.WriteLine()

			line = "$noforcedfade"
			theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteForcePhonemeCrossfadeCommand()
		Dim line As String = ""

		'$forcephonemecrossfade
		If (theSourceEngineModel.theMdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_FORCE_PHONEME_CROSSFADE) > 0 Then
			theOutputFileStream.WriteLine()

			line = "$forcephonemecrossfade"
			theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WritePoseParameterCommand()
		Dim line As String = ""

		'$poseparameter body_pitch -90.00 90.00 360.00
		'$poseparameter body_yaw -90.00 90.00 360.00
		'$poseparameter head_pitch -90.00 90.00 360.00
		'$poseparameter head_yaw -90.00 90.00 360.00
		If theSourceEngineModel.theMdlFileHeader.thePoseParamDescs IsNot Nothing Then
			line = ""
			theOutputFileStream.WriteLine(line)

			For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.thePoseParamDescs.Count - 1
				Dim aPoseParamDesc As SourceMdlPoseParamDesc
				aPoseParamDesc = theSourceEngineModel.theMdlFileHeader.thePoseParamDescs(i)
				line = "$poseparameter """
				line += aPoseParamDesc.theName
				line += """ "
				line += aPoseParamDesc.startingValue.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aPoseParamDesc.endingValue.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aPoseParamDesc.loopingRange.ToString("0.######", TheApp.InternalNumberFormat)
				theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	Private Sub WriteAmbientBoostCommand()
		Dim line As String = ""

		'$ambientboost
		If (theSourceEngineModel.theMdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_AMBIENT_BOOST) > 0 Then
			theOutputFileStream.WriteLine()

			line = "$ambientboost"
			theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteOpaqueCommand()
		Dim line As String = ""

		'$mostlyopaque
		'$opaque
		If (theSourceEngineModel.theMdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_FORCE_OPAQUE) > 0 Then
			theOutputFileStream.WriteLine()

			line = "$opaque"
			theOutputFileStream.WriteLine(line)
		ElseIf (theSourceEngineModel.theMdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_TRANSLUCENT_TWOPASS) > 0 Then
			theOutputFileStream.WriteLine()

			line = "$mostlyopaque"
			theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteObsoleteCommand()
		Dim line As String = ""

		'$obsolete
		If (theSourceEngineModel.theMdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_OBSOLETE) > 0 Then
			theOutputFileStream.WriteLine()

			line = "$obsolete"
			theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteCdMaterialsCommand()
		Dim line As String = ""

		'$cdmaterials "models\survivors\producer\"
		'$cdmaterials "models\survivors\"
		'$cdmaterials ""
		If theSourceEngineModel.theMdlFileHeader.theTexturePaths IsNot Nothing Then
			line = ""
			theOutputFileStream.WriteLine(line)

			For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theTexturePaths.Count - 1
				Dim aTexturePath As String
				aTexturePath = theSourceEngineModel.theMdlFileHeader.theTexturePaths(i)
				If Not String.IsNullOrEmpty(aTexturePath) Then
					line = "$cdmaterials "
					line += """"
					line += aTexturePath
					line += """"
					theOutputFileStream.WriteLine(line)
				End If
			Next
		End If
	End Sub

	Private Sub WriteTextureGroupCommand()
		Dim line As String = ""

		'$texturegroup skinfamilies
		'{
		'	{"producer_head.vmt"
		' "producer_body.vmt"
		' "producer_head_it.vmt"
		' "producer_body_it.vmt"
		'}
		' 	{"producer_head_it.vmt"
		' "producer_body_it.vmt"
		' "producer_head_it.vmt"
		' "producer_body_it.vmt"
		'}
		' }
		If theSourceEngineModel.theMdlFileHeader.theSkinFamilies IsNot Nothing AndAlso theSourceEngineModel.theMdlFileHeader.skinReferenceCount > 0 Then
			line = ""
			theOutputFileStream.WriteLine(line)

			line = "$texturegroup ""skinfamilies"""
			theOutputFileStream.WriteLine(line)
			line = "{"
			theOutputFileStream.WriteLine(line)
			For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theSkinFamilies.Count - 1
				Dim aSkinFamily As List(Of Integer)
				aSkinFamily = theSourceEngineModel.theMdlFileHeader.theSkinFamilies(i)

				line = vbTab
				line += "{"
				theOutputFileStream.WriteLine(line)

				'For j As Integer = 0 To theSourceEngineModel.theMdlFileData.theBodyParts(0).theModels(0).theMeshes.Count - 1
				For j As Integer = 0 To theSourceEngineModel.theMdlFileHeader.skinReferenceCount - 1
					'If aSourceEngineModel.theBodyParts(0).theModels(0).theMeshes(j).materialType = 0 Then
					Dim aTexture As SourceMdlTexture
					'aTexture = theSourceEngineModel.theMdlFileHeader.theTextures(j)
					aTexture = theSourceEngineModel.theMdlFileHeader.theTextures(aSkinFamily(j))
					line = vbTab
					line += vbTab
					line += """"
					line += aTexture.theName
					line += ".vmt"""
					theOutputFileStream.WriteLine(line)
					'End If
				Next

				line = vbTab
				line += "}"
				theOutputFileStream.WriteLine(line)
			Next
			line = "}"
			theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteTextureFileNameComments()
		'// Model uses material "producer_head.vmt"
		'// Model uses material "producer_body.vmt"
		'// Model uses material "producer_head_it.vmt"
		'// Model uses material "producer_body_it.vmt"
		'// Model uses material "models/survivors/producer/producer_hair.vmt"
		'// Model uses material "models/survivors/producer/producer_eyeball_l.vmt"
		'// Model uses material "models/survivors/producer/producer_eyeball_r.vmt"
		If theSourceEngineModel.theMdlFileHeader.theTextures IsNot Nothing Then
			Dim line As String

			line = ""
			theOutputFileStream.WriteLine(line)

			For j As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theTextures.Count - 1
				Dim aTexture As SourceMdlTexture
				aTexture = theSourceEngineModel.theMdlFileHeader.theTextures(j)
				line = "// Model uses material """
				line += aTexture.theName
				line += ".vmt"""
				theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	Private Sub WriteAttachmentCommand()
		Dim line As String = ""
		Dim offsetX As Double
		Dim offsetY As Double
		Dim offsetZ As Double
		Dim angleX As Double
		Dim angleY As Double
		Dim angleZ As Double

		'$attachment "eyes" "ValveBiped.Bip01_Head1" 3.42 -2.36 0.05 rotate 0.00 -89.37 -90.00
		'$attachment "mouth" "ValveBiped.Bip01_Head1" 0.71 -5.15 -0.13 rotate -0.00 -80.00 -90.00
		'$attachment "survivor_light" "ValveBiped.Bip01_Spine2" 5.33 21.31 -0.00 rotate -0.00 -0.00 -0.00
		'$attachment "forward" "ValveBiped.forward" 0.00 -0.00 0.00 rotate 0.00 0.00 0.00
		'$attachment "pistol" "ValveBiped.Bip01_R_Thigh" -2.95 1.84 -4.61 rotate -3.66 -0.47 91.70
		'$attachment "L_weapon_bone" "ValveBiped.L_weapon_bone" 0.00 -0.00 0.00 rotate -0.00 0.00 -0.00
		'$attachment "weapon_bone" "ValveBiped.weapon_bone" 0.00 0.00 0.00 rotate 0.00 0.00 -0.00
		'$attachment "medkit" "ValveBiped.Bip01_Spine4" -0.65 -2.83 -1.16 rotate 5.03 77.16 0.00
		'$attachment "primary" "ValveBiped.Bip01_Spine4" 2.71 -4.36 -2.33 rotate -13.70 170.19 174.29
		'$attachment "attach_R_shoulderBladeAim" "ValveBiped.Bip01_Spine4" -8.88 0.88 -4.51 rotate -90.00 -102.85 0.00
		'$attachment "attach_L_shoulderBladeAim" "ValveBiped.Bip01_Spine4" -8.88 0.88 3.12 rotate -90.00 -102.85 0.00
		'$attachment "melee" "ValveBiped.Bip01_Spine4" 2.64 -3.12 4.45 rotate 24.08 175.37 97.14
		'$attachment "molotov" "ValveBiped.Bip01_Spine" -3.19 -2.44 7.01 rotate -63.44 -74.67 -101.41
		'$attachment "grenade" "ValveBiped.Bip01_Spine" -0.68 1.17 6.97 rotate -90.00 -175.23 0.00
		'$attachment "pills" "ValveBiped.Bip01_Spine" -2.63 0.63 -7.56 rotate -41.18 -88.48 -87.05
		'$attachment "lfoot" "ValveBiped.Bip01_L_Foot" 0.00 4.44 0.00 rotate -0.00 -0.00 -0.00
		'$attachment "rfoot" "ValveBiped.Bip01_R_Foot" 0.00 4.44 0.00 rotate -0.00 0.00 -0.00
		'$attachment "muzzle_flash" "ValveBiped.Bip01_L_Hand" 0.00 0.00 0.00 rotate -0.00 0.00 0.00
		'$attachment "survivor_neck" "ValveBiped.Bip01_Neck1" 0.00 0.00 0.00 rotate 0.00 0.00 -0.00
		'$attachment "forward" "ValveBiped.forward" 0.00 -0.00 0.00 rotate 0.00 0.00 0.00
		'$attachment "bleedout" "ValveBiped.Bip01_Pelvis" 8.44 8.88 4.44 rotate -0.00 0.00 0.00
		'$attachment "survivor_light" "ValveBiped.Bip01_Spine2" 5.33 21.31 -0.00 rotate -0.00 -0.00 -0.00
		'$attachment "legL_B" "ValveBiped.attachment_bandage_legL" 0.00 0.00 0.00 rotate -90.00 -90.00 0.00
		'$attachment "armL_B" "ValveBiped.attachment_bandage_armL" 0.00 0.00 0.00 rotate -90.00 -90.00 0.00
		'$attachment "armL_T" "ValveBiped.attachment_armL_T" 0.00 0.00 0.00 rotate -90.00 -90.00 0.00
		'$attachment "armR_T" "ValveBiped.attachment_armR_T" 0.00 0.00 0.00 rotate -90.00 -90.00 0.00
		'$attachment "armL" "ValveBiped.Bip01_L_Forearm" 0.00 0.00 -0.00 rotate -0.00 -0.00 0.00
		'$attachment "legL" "ValveBiped.Bip01_L_Calf" 0.00 0.00 0.00 rotate -0.00 -0.00 -0.00
		'$attachment "thighL" "ValveBiped.Bip01_L_Thigh" 0.00 0.00 0.00 rotate -0.00 -0.00 -0.00
		'$attachment "spine" "ValveBiped.Bip01_Spine" 0.00 0.00 0.00 rotate -90.00 -90.00 0.00
		If theSourceEngineModel.theMdlFileHeader.theAttachments IsNot Nothing Then
			line = ""
			theOutputFileStream.WriteLine(line)

			For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theAttachments.Count - 1
				Dim anAttachment As SourceMdlAttachment
				anAttachment = theSourceEngineModel.theMdlFileHeader.theAttachments(i)
				line = "$attachment "
				line += """"
				line += anAttachment.theName
				line += """ """
				line += theSourceEngineModel.theMdlFileHeader.theBones(anAttachment.localBoneIndex).theName
				line += """"
				line += " "
				'TheApp.ConvertRotationMatrixToDegrees(anAttachment.localM11, anAttachment.localM12, anAttachment.localM13, anAttachment.localM21, anAttachment.localM22, anAttachment.localM23, anAttachment.localM33, angleX, angleY, angleZ)
				'NOTE: This one works with the strange order below.
				MathModule.ConvertRotationMatrixToDegrees(anAttachment.localM11, anAttachment.localM21, anAttachment.localM31, anAttachment.localM12, anAttachment.localM22, anAttachment.localM32, anAttachment.localM33, angleX, angleY, angleZ)
				offsetX = Math.Round(anAttachment.localM14, 2)
				offsetY = Math.Round(anAttachment.localM24, 2)
				offsetZ = Math.Round(anAttachment.localM34, 2)
				angleX = Math.Round(angleX, 2)
				angleY = Math.Round(angleY, 2)
				angleZ = Math.Round(angleZ, 2)
				line += offsetX.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += offsetY.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += offsetZ.ToString("0.######", TheApp.InternalNumberFormat)
				line += " rotate "
				''NOTE: Intentionally z,y,x order.
				'line += angleZ.ToString()
				'line += " "
				'line += angleY.ToString()
				'line += " "
				'line += angleX.ToString()
				'NOTE: Intentionally in strange order.
				line += angleY.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += (-angleZ).ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += (-angleX).ToString("0.######", TheApp.InternalNumberFormat)
				theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	Private Sub WriteIncludeModelCommand()
		Dim line As String = ""

		'$includemodel "survivors/anim_producer.mdl"
		'$includemodel "survivors/anim_gestures.mdl"
		If theSourceEngineModel.theMdlFileHeader.theModelGroups IsNot Nothing Then
			line = ""
			theOutputFileStream.WriteLine(line)

			For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theModelGroups.Count - 1
				Dim aModelGroup As SourceMdlModelGroup
				aModelGroup = theSourceEngineModel.theMdlFileHeader.theModelGroups(i)
				line = "$includemodel "
				line += """"
				If aModelGroup.theFileName.StartsWith("models/") Then
					line += aModelGroup.theFileName.Substring(7)
				Else
					line += aModelGroup.theFileName
				End If
				line += """"
				theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	Private Sub WriteSurfacePropCommand()
		Dim line As String = ""

		line = ""
		theOutputFileStream.WriteLine(line)

		'$surfaceprop "flesh"
		line = "$surfaceprop "
		line += """"
		line += theSourceEngineModel.theMdlFileHeader.theSurfacePropName
		line += """"
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteJointSurfacePropCommand()
		Dim line As String = ""

		'$jointsurfaceprop <bone name> <surfaceprop>
		'$jointsurfaceprop "ValveBiped.Bip01_L_Toe0"	 "flesh"
		If theSourceEngineModel.theMdlFileHeader.theBones IsNot Nothing Then
			Dim aBone As SourceMdlBone
			Dim emptyLineIsAlreadyWritten As Boolean

			emptyLineIsAlreadyWritten = False
			For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theBones.Count - 1
				aBone = theSourceEngineModel.theMdlFileHeader.theBones(i)

				If aBone.theSurfacePropName <> theSourceEngineModel.theMdlFileHeader.theSurfacePropName Then
					If Not emptyLineIsAlreadyWritten Then
						line = ""
						theOutputFileStream.WriteLine(line)
						emptyLineIsAlreadyWritten = True
					End If

					line = "$jointsurfaceprop "
					line += """"
					line += aBone.theName
					line += """"
					line += " "
					line += """"
					line += aBone.theSurfacePropName
					line += """"
					theOutputFileStream.WriteLine(line)
				End If
			Next
		End If
	End Sub

	Private Sub WriteContentsCommand()
		If Me.theSourceEngineModel.theMdlFileHeader.contents > 0 Then
			Dim line As String = ""

			line = ""
			theOutputFileStream.WriteLine(line)

			'$contents "monster" "grate"
			line = "$contents"
			line += Me.GetContentsFlags(Me.theSourceEngineModel.theMdlFileHeader.contents)
			theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteJointContentsCommand()
		Dim line As String = ""

		'$jointcontents "<bone_name>" "<content_type_1>" "<content_type_2>" "<content_type_3>"
		If theSourceEngineModel.theMdlFileHeader.theBones IsNot Nothing Then
			Dim aBone As SourceMdlBone
			Dim emptyLineIsAlreadyWritten As Boolean

			emptyLineIsAlreadyWritten = False
			For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theBones.Count - 1
				aBone = theSourceEngineModel.theMdlFileHeader.theBones(i)

				If aBone.contents <> Me.theSourceEngineModel.theMdlFileHeader.contents Then
					If Not emptyLineIsAlreadyWritten Then
						line = ""
						theOutputFileStream.WriteLine(line)
						emptyLineIsAlreadyWritten = True
					End If

					line = "$jointcontents"
					line += """"
					line += aBone.theName
					line += """"
					line += Me.GetContentsFlags(aBone.contents)
					theOutputFileStream.WriteLine(line)
				End If
			Next
		End If
	End Sub

	'//-----------------------------------------------------------------------------
	'// Parse contents flags
	'//-----------------------------------------------------------------------------
	'static void ParseContents( int *pAddFlags, int *pRemoveFlags )
	'{
	'	*pAddFlags = 0;
	'	*pRemoveFlags = 0;
	'	do 
	'	{
	'		GetToken (false);

	'		if ( !stricmp( token, "grate" ) )
	'		{
	'			*pAddFlags |= CONTENTS_GRATE;
	'			*pRemoveFlags |= CONTENTS_SOLID;
	'		}
	'		else if ( !stricmp( token, "ladder" ) )
	'		{
	'			*pAddFlags |= CONTENTS_LADDER;
	'		}
	'		else if ( !stricmp( token, "solid" ) )
	'		{
	'			*pAddFlags |= CONTENTS_SOLID;
	'		}
	'		else if ( !stricmp( token, "monster" ) )
	'		{
	'			*pAddFlags |= CONTENTS_MONSTER;
	'		}
	'		else if ( !stricmp( token, "notsolid" ) )
	'		{
	'			*pRemoveFlags |= CONTENTS_SOLID;
	'		}
	'	} while (TokenAvailable());
	'}
	Private Function GetContentsFlags(ByVal contentsFlags As Integer) As String
		Dim flagNames As String = ""

		If (contentsFlags And SourceMdlBone.CONTENTS_GRATE) > 0 Then
			flagNames += " "
			flagNames += """"
			flagNames += "grate"
			flagNames += """"
		End If
		If (contentsFlags And SourceMdlBone.CONTENTS_MONSTER) > 0 Then
			flagNames += " "
			flagNames += """"
			flagNames += "monster"
			flagNames += """"
		End If
		If (contentsFlags And SourceMdlBone.CONTENTS_LADDER) > 0 Then
			flagNames += " "
			flagNames += """"
			flagNames += "ladder"
			flagNames += """"
		End If
		If (contentsFlags And SourceMdlBone.CONTENTS_SOLID) > 0 Then
			flagNames += " "
			flagNames += """"
			flagNames += "solid"
			flagNames += """"
		End If

		If flagNames = "" Then
			flagNames += " "
			flagNames += """"
			flagNames += "notsolid"
			flagNames += """"
		End If

		Return flagNames
	End Function

	Private Sub WriteEyePositionCommand()
		Dim line As String = ""
		Dim offsetX As Double
		Dim offsetY As Double
		Dim offsetZ As Double

		offsetX = Math.Round(theSourceEngineModel.theMdlFileHeader.eyePositionY, 3)
		offsetY = -Math.Round(theSourceEngineModel.theMdlFileHeader.eyePositionX, 3)
		offsetZ = Math.Round(theSourceEngineModel.theMdlFileHeader.eyePositionZ, 3)

		If offsetX = 0 AndAlso offsetY = 0 AndAlso offsetZ = 0 Then
			Exit Sub
		End If

		line = ""
		theOutputFileStream.WriteLine(line)

		'$eyeposition -0.000 0.000 70.000
		'NOTE: These are stored in different order in MDL file.
		'FROM: utils\studiomdl\studiomdl.cpp Cmd_Eyeposition()
		'eyeposition[1] = verify_atof (token);
		'eyeposition[0] = -verify_atof (token);
		'eyeposition[2] = verify_atof (token);
		line = "$eyeposition "
		line += offsetX.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += offsetY.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += offsetZ.ToString("0.######", TheApp.InternalNumberFormat)
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteIllumPositionCommand()
		Dim line As String = ""
		Dim offsetX As Double
		Dim offsetY As Double
		Dim offsetZ As Double

		line = ""
		theOutputFileStream.WriteLine(line)

		line = ""
		line += "// "
		line += "Only set this if you know what it does, and need it for special circumstances, such as with gibs."
		theOutputFileStream.WriteLine(line)

		'$illumposition -2.533 -0.555 32.487
		'NOTE: These are stored in different order in MDL file.
		'FROM: utils\studiomdl\studiomdl.cpp Cmd_Illumposition()
		'illumposition[1] = verify_atof (token);
		'illumposition[0] = -verify_atof (token);
		'illumposition[2] = verify_atof (token);
		offsetX = Math.Round(theSourceEngineModel.theMdlFileHeader.illuminationPositionY, 3)
		offsetY = -Math.Round(theSourceEngineModel.theMdlFileHeader.illuminationPositionX, 3)
		offsetZ = Math.Round(theSourceEngineModel.theMdlFileHeader.illuminationPositionZ, 3)
		line = ""
		line += "// "
		line += "$illumposition "
		line += offsetX.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += offsetY.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += offsetZ.ToString("0.######", TheApp.InternalNumberFormat)
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteSectionFramesCommand()
		Dim line As String = ""

		'$sectionframes
		If theSourceEngineModel.theMdlFileHeader.theSectionFrameCount > 0 Then
			theOutputFileStream.WriteLine()

			line = "$sectionframes"
			line += " "
			line += theSourceEngineModel.theMdlFileHeader.theSectionFrameCount.ToString(TheApp.InternalNumberFormat)
			line += " "
			line += theSourceEngineModel.theMdlFileHeader.theSectionFrameMinFrameCount.ToString(TheApp.InternalNumberFormat)
			theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteAnimationAndDeclareAnimationCommand()
		Dim line As String = ""

		If theSourceEngineModel.theMdlFileHeader.theAnimationDescs IsNot Nothing Then
			theOutputFileStream.WriteLine()

			Me.theFirstAnimationDescName = ""
			For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theAnimationDescs.Count - 1
				Dim anAnimDesc As SourceMdlAnimationDesc
				anAnimDesc = theSourceEngineModel.theMdlFileHeader.theAnimationDescs(i)

				If (anAnimDesc.flags And SourceMdlAnimationDesc.STUDIO_OVERRIDE) > 0 Then
					line = "$declareanimation"
					line += " """
					'TODO: Does this need to check and remove initial "@" from name?
					line += anAnimDesc.theName
					line += """"
					theOutputFileStream.WriteLine(line)

					Continue For
				End If

				If Me.theFirstAnimationDescName = "" AndAlso anAnimDesc.theName(0) <> "@" Then
					Me.theFirstAnimationDescName = anAnimDesc.theName
				End If

				'If anAnimDesc.theName(0) <> "@" AndAlso Not anAnimDesc.theAnimIsLinkedToSequence Then
				If anAnimDesc.theName(0) <> "@" Then
					Me.WriteAnimationLine(anAnimDesc)
				End If
			Next
		End If
	End Sub

	Private Sub WriteSequenceAndDeclareSequenceCommand()
		Dim line As String = ""
		Dim startAnimDescIndex As Integer
		Dim valueString As String
		Dim firstAnimDesc As SourceMdlAnimationDesc

		'$sequence producer "producer" fps 30.00
		'$sequence ragdoll "ragdoll" ACT_DIERAGDOLL 1 fps 30.00
		If theSourceEngineModel.theMdlFileHeader.theSequenceDescs IsNot Nothing Then
			theOutputFileStream.WriteLine()

			For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theSequenceDescs.Count - 1
				Dim aSeqDesc As SourceMdlSequenceDesc
				aSeqDesc = theSourceEngineModel.theMdlFileHeader.theSequenceDescs(i)

				If (aSeqDesc.flags And SourceMdlAnimationDesc.STUDIO_OVERRIDE) > 0 Then
					line = "$declaresequence"
					line += " """
					line += aSeqDesc.theName
					line += """"
					theOutputFileStream.WriteLine(line)

					Continue For
				End If

				If aSeqDesc.theAnimDescIndexes Is Nothing OrElse aSeqDesc.theAnimDescIndexes.Count < 1 Then
					Continue For
				End If
				firstAnimDesc = theSourceEngineModel.theMdlFileHeader.theAnimationDescs(aSeqDesc.theAnimDescIndexes(0))

				theOutputFileStream.WriteLine()

				'If aSeqDesc.theAnimDescIndexes IsNot Nothing Then
				'	For j As Integer = 0 To aSeqDesc.theAnimDescIndexes.Count - 1
				'		Me.WriteSequenceAnimationCommand(aSeqDesc.theAnimDescIndexes(j))
				'	Next
				'End If

				line = "$sequence"
				line += " """
				line += aSeqDesc.theName
				line += """"
				startAnimDescIndex = 0
				If aSeqDesc.theAnimDescIndexes IsNot Nothing Then
					Dim name As String
					name = firstAnimDesc.theName
					If name(0) = "@" Then
						line += " """
						''NOTE: Anim smd files are placed in a subfolder.
						'line += App.AnimsSubFolderName
						'line += Path.DirectorySeparatorChar
						'line += name.Substring(1)
						line += theSourceEngineModel.GetAnimationSmdRelativePathFileName(firstAnimDesc)
						line += """"
						If name.Substring(1) = aSeqDesc.theName Then
							startAnimDescIndex = 1
						End If
						'NOTE: Should NOT need this, based on L4D2 starter kits qc files.
						'Else
						'	'TODO: Not sure what to use here, but this works for now.
						'	line += " """
						'	line += name
						'	line += """"
					End If
				End If

				line += " {"
				theOutputFileStream.WriteLine(line)

				If aSeqDesc.theAnimDescIndexes IsNot Nothing Then
					For j As Integer = startAnimDescIndex To aSeqDesc.theAnimDescIndexes.Count - 1
						line = vbTab
						line += """"
						line += theSourceEngineModel.theMdlFileHeader.theAnimationDescs(aSeqDesc.theAnimDescIndexes(j)).theName
						line += """"
						theOutputFileStream.WriteLine(line)
					Next
				End If

				line = vbTab
				line += "fps "
				line += firstAnimDesc.fps.ToString("0.######", TheApp.InternalNumberFormat)
				theOutputFileStream.WriteLine(line)

				If (firstAnimDesc.flags And SourceMdlAnimationDesc.STUDIO_DELTA) > 0 Then
					line = vbTab
					line += "// "
					line += "subtract"
					line += " """
					'TODO: Change to writing anim_name.
					' Doesn't seem to be direct way to get this name.
					' For now, do what MDL Decompiler seems to do; use the first animation name.
					'line += "[anim_name]"
					line += Me.theFirstAnimationDescName
					line += """ "
					'TODO: Change to writing frameIndex.
					' Doesn't seem to be direct way to get this value.
					' For now, do what MDL Decompiler seems to do; use zero for the frameIndex.
					'line += "[frameIndex]"
					line += "0"
					theOutputFileStream.WriteLine(line)
				End If

				If aSeqDesc.theActivityName <> "" Then
					line = vbTab
					line += """"
					line += aSeqDesc.theActivityName
					line += """ "
					line += aSeqDesc.activityWeight.ToString(TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)
				End If

				If (aSeqDesc.flags And SourceMdlAnimationDesc.STUDIO_LOOPING) > 0 Then
					line = vbTab
					line += "loop"
					theOutputFileStream.WriteLine(line)
				End If

				If (aSeqDesc.flags And SourceMdlAnimationDesc.STUDIO_SNAP) > 0 Then
					line = vbTab
					line += "snap"
					theOutputFileStream.WriteLine(line)
				End If

				Me.WriteSequenceDeltaInfo(aSeqDesc)

				If (aSeqDesc.flags And SourceMdlAnimationDesc.STUDIO_WORLD) > 0 Then
					line = vbTab
					line += "worldspace"
					theOutputFileStream.WriteLine(line)
				End If

				If (aSeqDesc.flags And SourceMdlAnimationDesc.STUDIO_AUTOPLAY) > 0 Then
					line = vbTab
					line += "autoplay"
					theOutputFileStream.WriteLine(line)
				End If

				If (aSeqDesc.flags And SourceMdlAnimationDesc.STUDIO_REALTIME) > 0 Then
					line = vbTab
					line += "realtime"
					theOutputFileStream.WriteLine(line)
				End If

				If (aSeqDesc.flags And SourceMdlAnimationDesc.STUDIO_HIDDEN) > 0 Then
					line = vbTab
					line += "hidden"
					theOutputFileStream.WriteLine(line)
				End If

				Me.WriteSequenceNodeInfo(aSeqDesc)

				If aSeqDesc.groupSize(0) <> aSeqDesc.groupSize(1) Then
					line = vbTab
					line += "blendwidth "
					line += aSeqDesc.groupSize(0).ToString(TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)
				End If

				Me.WriteSequenceBlendInfo(aSeqDesc)

				Me.WriteSequenceLayerInfo(aSeqDesc)

				If aSeqDesc.theIkLocks IsNot Nothing AndAlso Me.theSourceEngineModel.theMdlFileHeader.theIkLocks IsNot Nothing AndAlso Me.theSourceEngineModel.theMdlFileHeader.theIkChains IsNot Nothing Then
					Dim ikLock As SourceMdlIkLock

					For ikLockIndex As Integer = 0 To aSeqDesc.theIkLocks.Count - 1
						If ikLockIndex >= Me.theSourceEngineModel.theMdlFileHeader.theIkLocks.Count Then
							Continue For
						End If
						ikLock = Me.theSourceEngineModel.theMdlFileHeader.theIkLocks(ikLockIndex)

						'iklock <chain name> <pos lock> <angle lock>
						line = vbTab
						line += "iklock """
						line += Me.theSourceEngineModel.theMdlFileHeader.theIkChains(ikLock.chainIndex).theName
						line += """"
						line += " "
						line += ikLock.posWeight.ToString("0.######", TheApp.InternalNumberFormat)
						line += " "
						line += ikLock.localQWeight.ToString("0.######", TheApp.InternalNumberFormat)
						theOutputFileStream.WriteLine(line)
					Next
				End If

				Me.WriteKeyValues(aSeqDesc.theKeyValues, "keyvalues")

				valueString = aSeqDesc.fadeInTime.ToString("0.######", TheApp.InternalNumberFormat)
				If valueString <> "0.2" Then
					line = vbTab
					line += "fadein "
					line += valueString
					theOutputFileStream.WriteLine(line)
				End If

				valueString = aSeqDesc.fadeOutTime.ToString("0.######", TheApp.InternalNumberFormat)
				If valueString <> "0.2" Then
					line = vbTab
					line += "fadeout "
					line += valueString
					theOutputFileStream.WriteLine(line)
				End If

				If aSeqDesc.theEvents IsNot Nothing Then
					Dim frameIndex As Double
					For j As Integer = 0 To aSeqDesc.theEvents.Count - 1
						frameIndex = aSeqDesc.theEvents(j).cycle * (theSourceEngineModel.theMdlFileHeader.theAnimationDescs(aSeqDesc.theAnimDescIndexes(0)).frameCount - 1)
						line = vbTab
						line += "{ "
						line += "event "
						line += aSeqDesc.theEvents(j).theName
						line += " "
						line += frameIndex.ToString(TheApp.InternalNumberFormat)
						If aSeqDesc.theEvents(j).options <> "" Then
							line += " """
							line += CStr(aSeqDesc.theEvents(j).options).Trim(Chr(0))
							line += """"
						End If
						line += " }"
						theOutputFileStream.WriteLine(line)
					Next
				End If

				'If blah Then
				'	line = vbTab
				'	line += ""
				'	theOutputFileStream.WriteLine(line)
				'End If

				line = "}"
				theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	Private Sub WriteSequenceDeltaInfo(ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim line As String = ""

		If (aSeqDesc.flags And SourceMdlAnimationDesc.STUDIO_DELTA) > 0 Then
			If (aSeqDesc.flags And SourceMdlAnimationDesc.STUDIO_POST) > 0 Then
				line = vbTab
				line += "// "
				line += "delta"
				theOutputFileStream.WriteLine(line)
			Else
				line = vbTab
				line += "predelta"
				theOutputFileStream.WriteLine(line)
			End If
		End If
	End Sub

	Private Sub WriteSequenceNodeInfo(ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim line As String = ""

		If aSeqDesc.localEntryNodeIndex > 0 Then
			If aSeqDesc.localEntryNodeIndex = aSeqDesc.localExitNodeIndex Then
				'node (name)
				line = vbTab
				line += "node"
				line += " """
				'NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
				line += theSourceEngineModel.theMdlFileHeader.theLocalNodeNames(aSeqDesc.localEntryNodeIndex - 1)
				line += """"
				theOutputFileStream.WriteLine(line)
			ElseIf (aSeqDesc.nodeFlags And 1) = 0 Then
				'transition (from) (to) 
				line = vbTab
				line += "transition"
				line += " """
				'NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
				line += theSourceEngineModel.theMdlFileHeader.theLocalNodeNames(aSeqDesc.localEntryNodeIndex - 1)
				line += """ """
				'NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
				line += theSourceEngineModel.theMdlFileHeader.theLocalNodeNames(aSeqDesc.localExitNodeIndex - 1)
				line += """"
				theOutputFileStream.WriteLine(line)
			Else
				'rtransition (name1) (name2) 
				line = vbTab
				line += "rtransition"
				line += " """
				'NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
				line += theSourceEngineModel.theMdlFileHeader.theLocalNodeNames(aSeqDesc.localEntryNodeIndex - 1)
				line += """ """
				'NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
				line += theSourceEngineModel.theMdlFileHeader.theLocalNodeNames(aSeqDesc.localExitNodeIndex - 1)
				line += """"
				theOutputFileStream.WriteLine(line)
			End If
		End If
	End Sub

	Private Sub WriteSequenceBlendInfo(ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim line As String = ""

		For i As Integer = 0 To 1
			If aSeqDesc.paramIndex(i) <> -1 Then
				line = vbTab
				line += "blend "
				line += """"
				line += theSourceEngineModel.theMdlFileHeader.thePoseParamDescs(aSeqDesc.paramIndex(i)).theName
				line += """"
				line += " "
				line += aSeqDesc.paramStart(i).ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aSeqDesc.paramEnd(i).ToString("0.######", TheApp.InternalNumberFormat)
				theOutputFileStream.WriteLine(line)
			End If
		Next
	End Sub

	Private Sub WriteSequenceLayerInfo(ByVal aSeqDesc As SourceMdlSequenceDesc)
		If aSeqDesc.autoLayerCount > 0 Then
			Dim line As String = ""
			Dim layer As SourceMdlAutoLayer
			Dim otherSequenceName As String

			For j As Integer = 0 To aSeqDesc.theAutoLayers.Count - 1
				layer = aSeqDesc.theAutoLayers(j)
				otherSequenceName = theSourceEngineModel.theMdlFileHeader.theSequenceDescs(layer.sequenceIndex).theName

				If layer.flags = 0 Then
					'addlayer <string|other $sequence name>
					line = vbTab
					line += "// "
					line += "addlayer "
					line += """"
					line += otherSequenceName
					line += """"
					theOutputFileStream.WriteLine(line)
				Else
					'blendlayer <string|other $sequence name> <int|startframe> <int|peakframe> <int|tailframe> <int|endframe> [spline] [xfade]
					line = vbTab
					line += "blendlayer "
					line += """"
					line += otherSequenceName
					line += """"

					line += " "
					line += layer.influenceStart.ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += layer.influencePeak.ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += layer.influenceTail.ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += layer.influenceEnd.ToString("0.######", TheApp.InternalNumberFormat)

					If (layer.flags And SourceMdlAutoLayer.STUDIO_AL_XFADE) > 0 Then
						line += " xfade"
					End If
					If (layer.flags And SourceMdlAutoLayer.STUDIO_AL_SPLINE) > 0 Then
						line += " spline"
					End If
					If (layer.flags And SourceMdlAutoLayer.STUDIO_AL_NOBLEND) > 0 Then
						line += " noblend"
					End If
					If (layer.flags And SourceMdlAutoLayer.STUDIO_AL_POSE) > 0 Then
						If Me.theSourceEngineModel.theMdlFileHeader.thePoseParamDescs IsNot Nothing AndAlso Me.theSourceEngineModel.theMdlFileHeader.thePoseParamDescs.Count > layer.poseIndex Then
							line += " poseparameter"
							line += " "
							line += Me.theSourceEngineModel.theMdlFileHeader.thePoseParamDescs(layer.poseIndex).theName
						End If
					End If
					If (layer.flags And SourceMdlAutoLayer.STUDIO_AL_LOCAL) > 0 Then
						line += " local"
					End If

					theOutputFileStream.WriteLine(line)
				End If
			Next
		End If
	End Sub

	'Private Sub WriteSequenceAnimationCommand(ByVal animDescIndex As Integer)
	'	Dim anAnimDesc As SourceMdlAnimationDesc
	'	anAnimDesc = theSourceEngineModel.theMdlFileHeader.theAnimationDescs(animDescIndex)

	'	If anAnimDesc.theName(0) <> "@" Then
	'		Me.WriteAnimationLine(anAnimDesc)
	'	End If
	'End Sub

	Private Sub WriteAnimationLine(ByVal anAnimationDesc As SourceMdlAnimationDesc)
		Dim line As String = ""

		line = "$animation"
		line += " """
		line += anAnimationDesc.theName
		line += """ """
		''NOTE: Anim smd files are placed in a subfolder.
		'line += App.AnimsSubFolderName
		'line += Path.DirectorySeparatorChar
		''NOTE: The file name for the animation data file is not stored in mdl file (which makes sense), 
		''      so make the file name the same as the animation name.
		'line += anAnimationDesc.theName
		''line += ".smd"
		line += Me.theSourceEngineModel.GetAnimationSmdRelativePathFileName(anAnimationDesc)
		line += """"
		line += " {"
		theOutputFileStream.WriteLine(line)

		line = vbTab
		line += "fps "
		line += anAnimationDesc.fps.ToString("0.######", TheApp.InternalNumberFormat)
		theOutputFileStream.WriteLine(line)

		If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_LOOPING) > 0 Then
			line = vbTab
			line += "loop"
			theOutputFileStream.WriteLine(line)
		End If

		If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_DELTA) > 0 Then
			line = vbTab
			line += "// "
			line += "subtract"
			line += " """
			'TODO: Change to writing anim_name.
			' Doesn't seem to be direct way to get this name.
			' For now, do what MDL Decompiler seems to do; use the first animation name.
			'line += "[anim_name]"
			line += Me.theFirstAnimationDescName
			line += """ "
			'TODO: Change to writing frameIndex.
			' Doesn't seem to be direct way to get this value.
			' For now, do what MDL Decompiler seems to do; use zero for the frameIndex.
			'line += "[frameIndex]"
			line += "0"
			theOutputFileStream.WriteLine(line)
		End If

		line = "}"
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteIkChainCommand()
		Dim line As String = ""
		Dim offsetX As Double
		Dim offsetY As Double
		Dim offsetZ As Double

		'$ikchain rhand ValveBiped.Bip01_R_Hand knee  0.707 0.707 0.000
		'$ikchain lhand ValveBiped.Bip01_L_Hand knee  0.707 0.707 0.000
		'$ikchain rfoot ValveBiped.Bip01_R_Foot knee  0.707 -0.707 0.000
		'$ikchain lfoot ValveBiped.Bip01_L_Foot knee  0.707 -0.707 0.000
		'$ikchain ikclip ValveBiped.weapon_bone_Clip knee  0.707 -0.707 0.000
		Try
			If theSourceEngineModel.theMdlFileHeader.theIkChains IsNot Nothing Then
				line = ""
				theOutputFileStream.WriteLine(line)

				For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theIkChains.Count - 1
					Dim boneIndex As Integer = theSourceEngineModel.theMdlFileHeader.theIkChains(i).theLinks(theSourceEngineModel.theMdlFileHeader.theIkChains(i).theLinks.Count - 1).boneIndex
					offsetX = Math.Round(theSourceEngineModel.theMdlFileHeader.theIkChains(i).theLinks(0).idealBendingDirection.x, 3)
					offsetY = Math.Round(theSourceEngineModel.theMdlFileHeader.theIkChains(i).theLinks(0).idealBendingDirection.y, 3)
					offsetZ = Math.Round(theSourceEngineModel.theMdlFileHeader.theIkChains(i).theLinks(0).idealBendingDirection.z, 3)

					line = "$ikchain """
					line += theSourceEngineModel.theMdlFileHeader.theIkChains(i).theName
					line += """ """
					line += theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).theName
					line += """ knee "
					line += offsetX.ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += offsetY.ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += offsetZ.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)
				Next
			End If
		Catch ex As Exception

		End Try
	End Sub

	Private Sub WriteIkAutoPlayLockCommand()
		Dim line As String = ""
		Dim ikLock As SourceMdlIkLock

		'$ikautoplaylock <chain name> <pos lock> <angle lock>
		'$ikautoplaylock rfoot 1.0 0.1
		'$ikautoplaylock lfoot 1.0 0.1
		Try
			If Me.theSourceEngineModel.theMdlFileHeader.theIkLocks IsNot Nothing Then
				line = ""
				theOutputFileStream.WriteLine(line)

				For i As Integer = 0 To Me.theSourceEngineModel.theMdlFileHeader.theIkLocks.Count - 1
					ikLock = Me.theSourceEngineModel.theMdlFileHeader.theIkLocks(i)

					line = "$ikautoplaylock """
					line += Me.theSourceEngineModel.theMdlFileHeader.theIkChains(ikLock.chainIndex).theName
					line += """"
					line += " "
					line += ikLock.posWeight.ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += ikLock.localQWeight.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)
				Next
			End If
		Catch ex As Exception

		End Try
	End Sub

	Private Sub WriteBoneSaveFrameCommand()
		Dim line As String = ""

		'$bonesaveframe <bone name> ["position"] ["rotation"]
		'$BoneSaveFrame "Dog_Model.Pelvis" position rotation
		'$BoneSaveFrame "Dog_Model.Leg1_L" rotation
		Try
			If Me.theSourceEngineModel.theMdlFileHeader.theBones IsNot Nothing Then
				Dim aBone As SourceMdlBone
				Dim emptyLineIsAlreadyWritten As Boolean

				emptyLineIsAlreadyWritten = False
				For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theBones.Count - 1
					aBone = theSourceEngineModel.theMdlFileHeader.theBones(i)

					If (aBone.flags And SourceMdlBone.BONE_HAS_SAVEFRAME_POS) > 0 OrElse (aBone.flags And SourceMdlBone.BONE_HAS_SAVEFRAME_ROT) > 0 Then
						If Not emptyLineIsAlreadyWritten Then
							line = ""
							theOutputFileStream.WriteLine(line)
							emptyLineIsAlreadyWritten = True
						End If

						line = "$bonesaveframe "
						line += """"
						line += aBone.theName
						line += """"
						If (aBone.flags And SourceMdlBone.BONE_HAS_SAVEFRAME_POS) > 0 Then
							line += " "
							line += "position"
						End If
						If (aBone.flags And SourceMdlBone.BONE_HAS_SAVEFRAME_ROT) > 0 Then
							line += " "
							line += "rotation"
						End If
						theOutputFileStream.WriteLine(line)
					End If
				Next
			End If
		Catch ex As Exception

		End Try
	End Sub

	Private Sub WriteCollisionModelOrCollisionJointsCommand()
		Dim line As String = ""

		'NOTE: Data is from PHY file.
		'$collisionmodel "tree_deciduous_01a_physbox.smd"
		'{
		'	$mass 350.0
		'	$concave
		'}	
		'$collisionjoints "phymodel.smd"
		'{
		'	$mass 100.0
		'	$inertia 10.00
		'	$damping 0.05
		'	$rotdamping 5.00
		'	$rootbone "valvebiped.bip01_pelvis"
		'	$jointrotdamping "valvebiped.bip01_pelvis" 3.00
		'
		'	$jointmassbias "valvebiped.bip01_spine1" 8.00
		'	$jointconstrain "valvebiped.bip01_spine1" x limit -10.00 10.00 0.00
		'	$jointconstrain "valvebiped.bip01_spine1" y limit -16.00 16.00 0.00
		'	$jointconstrain "valvebiped.bip01_spine1" z limit -20.00 30.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_spine2" 9.00
		'	$jointconstrain "valvebiped.bip01_spine2" x limit -10.00 10.00 0.00
		'	$jointconstrain "valvebiped.bip01_spine2" y limit -10.00 10.00 0.00
		'	$jointconstrain "valvebiped.bip01_spine2" z limit -20.00 20.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_r_clavicle" 4.00
		'	$jointrotdamping "valvebiped.bip01_r_clavicle" 6.00
		'	$jointconstrain "valvebiped.bip01_r_clavicle" x limit -15.00 15.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_clavicle" y limit -10.00 10.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_clavicle" z limit -0.00 45.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_l_clavicle" 4.00
		'	$jointrotdamping "valvebiped.bip01_l_clavicle" 6.00
		'	$jointconstrain "valvebiped.bip01_l_clavicle" x limit -15.00 15.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_clavicle" y limit -10.00 10.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_clavicle" z limit -0.00 45.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_l_upperarm" 5.00
		'	$jointrotdamping "valvebiped.bip01_l_upperarm" 2.00
		'	$jointconstrain "valvebiped.bip01_l_upperarm" x limit -15.00 20.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_upperarm" y limit -40.00 32.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_upperarm" z limit -80.00 25.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_l_forearm" 4.00
		'	$jointrotdamping "valvebiped.bip01_l_forearm" 4.00
		'	$jointconstrain "valvebiped.bip01_l_forearm" x limit -40.00 15.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_forearm" y limit 0.00 0.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_forearm" z limit -120.00 10.00 0.00
		'
		'	$jointrotdamping "valvebiped.bip01_l_hand" 1.00
		'	$jointconstrain "valvebiped.bip01_l_hand" x limit -25.00 25.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_hand" y limit -35.00 35.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_hand" z limit -50.00 50.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_r_upperarm" 5.00
		'	$jointrotdamping "valvebiped.bip01_r_upperarm" 2.00
		'	$jointconstrain "valvebiped.bip01_r_upperarm" x limit -15.00 20.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_upperarm" y limit -40.00 32.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_upperarm" z limit -80.00 25.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_r_forearm" 4.00
		'	$jointrotdamping "valvebiped.bip01_r_forearm" 4.00
		'	$jointconstrain "valvebiped.bip01_r_forearm" x limit -40.00 15.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_forearm" y limit 0.00 0.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_forearm" z limit -120.00 10.00 0.00
		'
		'	$jointrotdamping "valvebiped.bip01_r_hand" 1.00
		'	$jointconstrain "valvebiped.bip01_r_hand" x limit -25.00 25.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_hand" y limit -35.00 35.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_hand" z limit -50.00 50.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_r_thigh" 7.00
		'	$jointrotdamping "valvebiped.bip01_r_thigh" 7.00
		'	$jointconstrain "valvebiped.bip01_r_thigh" x limit -25.00 25.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_thigh" y limit -10.00 15.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_thigh" z limit -55.00 25.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_r_calf" 4.00
		'	$jointconstrain "valvebiped.bip01_r_calf" x limit -10.00 25.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_calf" y limit -5.00 5.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_calf" z limit -10.00 115.00 0.00
		'
		'	$jointrotdamping "valvebiped.bip01_r_foot" 2.00
		'	$jointconstrain "valvebiped.bip01_r_foot" x limit -20.00 30.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_foot" y limit -30.00 20.00 0.00
		'	$jointconstrain "valvebiped.bip01_r_foot" z limit -30.00 50.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_l_thigh" 7.00
		'	$jointrotdamping "valvebiped.bip01_l_thigh" 7.00
		'	$jointconstrain "valvebiped.bip01_l_thigh" x limit -25.00 25.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_thigh" y limit -10.00 15.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_thigh" z limit -55.00 25.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_l_calf" 4.00
		'	$jointconstrain "valvebiped.bip01_l_calf" x limit -10.00 25.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_calf" y limit -5.00 5.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_calf" z limit -10.00 115.00 0.00
		'
		'	$jointrotdamping "valvebiped.bip01_l_foot" 2.00
		'	$jointconstrain "valvebiped.bip01_l_foot" x limit -20.00 30.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_foot" y limit -30.00 20.00 0.00
		'	$jointconstrain "valvebiped.bip01_l_foot" z limit -30.00 50.00 0.00
		'
		'	$jointmassbias "valvebiped.bip01_head1" 4.00
		'	$jointrotdamping "valvebiped.bip01_head1" 3.00
		'	$jointconstrain "valvebiped.bip01_head1" x limit -50.00 50.00 0.00
		'	$jointconstrain "valvebiped.bip01_head1" y limit -20.00 20.00 0.00
		'	$jointconstrain "valvebiped.bip01_head1" z limit -26.00 30.00 0.00
		'}
		If theSourceEngineModel.thePhyFileHeader IsNot Nothing AndAlso Me.theSourceEngineModel.thePhyFileHeader.solidCount > 0 Then
			If Me.theSourceEngineModel.thePhyFileHeader.checksum = Me.theSourceEngineModel.theMdlFileHeader.checksum Then
				line = ""
				theOutputFileStream.WriteLine(line)

				'NOTE: The smd file name for $collisionjoints is not stored in the mdl file, 
				'      so use the same name that MDL Decompiler uses.
				'TODO: Find a better way to determine which to use.
				'NOTE: "If Me.theSourceEngineModel.theMdlFileHeader.theAnimationDescs.Count < 2" 
				'      works for survivors but not for witch (which has only one sequence).
				If theSourceEngineModel.thePhyFileHeader.theSourcePhyPhysCollisionModels.Count < 2 Then
					line = "$collisionmodel "
				Else
					line = "$collisionjoints "
				End If
				'line += """phymodel.smd"""
				line += """"
				line += theSourceEngineModel.GetPhysicsSmdFileName()
				line += """"
				theOutputFileStream.WriteLine(line)
				line = "{"
				theOutputFileStream.WriteLine(line)

				line = vbTab
				line += "$mass "
				line += Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyEditParamsSection.totalMass.ToString("0.######", TheApp.InternalNumberFormat)
				theOutputFileStream.WriteLine(line)
				line = vbTab
				line += "$inertia "
				line += Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyPhysCollisionModelMostUsedValues.theInertia.ToString("0.######", TheApp.InternalNumberFormat)
				theOutputFileStream.WriteLine(line)
				line = vbTab
				line += "$damping "
				line += Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyPhysCollisionModelMostUsedValues.theDamping.ToString("0.######", TheApp.InternalNumberFormat)
				theOutputFileStream.WriteLine(line)
				line = vbTab
				line += "$rotdamping "
				line += Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyPhysCollisionModelMostUsedValues.theRotDamping.ToString("0.######", TheApp.InternalNumberFormat)
				theOutputFileStream.WriteLine(line)
				If Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyEditParamsSection.rootName <> "" Then
					line = vbTab
					line += "$rootbone """
					line += Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyEditParamsSection.rootName
					line += """"
					theOutputFileStream.WriteLine(line)
				End If
				If Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyEditParamsSection.concave = "1" Then
					line = vbTab
					line += "$concave"
					theOutputFileStream.WriteLine(line)
				End If

				For i As Integer = 0 To Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyPhysCollisionModels.Count - 1
					Dim aSourcePhysCollisionModel As SourcePhyPhysCollisionModel
					aSourcePhysCollisionModel = Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyPhysCollisionModels(i)

					line = ""
					theOutputFileStream.WriteLine(line)

					If aSourcePhysCollisionModel.theMassBiasIsValid Then
						line = vbTab
						line += "$jointmassbias """
						line += aSourcePhysCollisionModel.theName
						line += """ "
						line += aSourcePhysCollisionModel.theMassBias.ToString("0.######", TheApp.InternalNumberFormat)
						theOutputFileStream.WriteLine(line)
					End If

					If aSourcePhysCollisionModel.theDamping <> Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyPhysCollisionModelMostUsedValues.theDamping Then
						line = vbTab
						line += "$jointdamping """
						line += aSourcePhysCollisionModel.theName
						line += """ "
						line += aSourcePhysCollisionModel.theDamping.ToString("0.######", TheApp.InternalNumberFormat)
						theOutputFileStream.WriteLine(line)
					End If

					If aSourcePhysCollisionModel.theInertia <> Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyPhysCollisionModelMostUsedValues.theInertia Then
						line = vbTab
						line += "$jointinertia """
						line += aSourcePhysCollisionModel.theName
						line += """ "
						line += aSourcePhysCollisionModel.theInertia.ToString("0.######", TheApp.InternalNumberFormat)
						theOutputFileStream.WriteLine(line)
					End If

					If aSourcePhysCollisionModel.theRotDamping <> Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyPhysCollisionModelMostUsedValues.theRotDamping Then
						line = vbTab
						line += "$jointrotdamping """
						line += aSourcePhysCollisionModel.theName
						line += """ "
						line += aSourcePhysCollisionModel.theRotDamping.ToString("0.######", TheApp.InternalNumberFormat)
						theOutputFileStream.WriteLine(line)
					End If

					If Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyRagdollConstraintDescs.ContainsKey(aSourcePhysCollisionModel.theIndex) Then
						Dim aConstraint As SourcePhyRagdollConstraint
						aConstraint = Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyRagdollConstraintDescs(aSourcePhysCollisionModel.theIndex)
						line = vbTab
						line += "$jointconstrain """
						line += aSourcePhysCollisionModel.theName
						line += """ x limit "
						line += aConstraint.theXMin.ToString("0.######", TheApp.InternalNumberFormat)
						line += " "
						line += aConstraint.theXMax.ToString("0.######", TheApp.InternalNumberFormat)
						line += " "
						line += aConstraint.theXFriction.ToString("0.######", TheApp.InternalNumberFormat)
						theOutputFileStream.WriteLine(line)
						line = vbTab
						line += "$jointconstrain """
						line += aSourcePhysCollisionModel.theName
						line += """ y limit "
						line += aConstraint.theYMin.ToString("0.######", TheApp.InternalNumberFormat)
						line += " "
						line += aConstraint.theYMax.ToString("0.######", TheApp.InternalNumberFormat)
						line += " "
						line += aConstraint.theYFriction.ToString("0.######", TheApp.InternalNumberFormat)
						theOutputFileStream.WriteLine(line)
						line = vbTab
						line += "$jointconstrain """
						line += aSourcePhysCollisionModel.theName
						line += """ z limit "
						line += aConstraint.theZMin.ToString("0.######", TheApp.InternalNumberFormat)
						line += " "
						line += aConstraint.theZMax.ToString("0.######", TheApp.InternalNumberFormat)
						line += " "
						line += aConstraint.theZFriction.ToString("0.######", TheApp.InternalNumberFormat)
						theOutputFileStream.WriteLine(line)
					End If
				Next

				If Not Me.theSourceEngineModel.thePhyFileHeader.theSourcePhySelfCollides Then
					line = vbTab
					line += "$noselfcollisions"
					theOutputFileStream.WriteLine(line)
				End If

				line = "}"
				theOutputFileStream.WriteLine(line)
			Else
				line = "// The PHY file's checksum value is not the same as the MDL file's checksum value."
				theOutputFileStream.WriteLine(line)
			End If
		End If
	End Sub

	Private Sub WriteCollisionTextCommand()
		Dim line As String = ""

		Try
			If theSourceEngineModel.thePhyFileHeader.theSourcePhyCollisionText IsNot Nothing AndAlso theSourceEngineModel.thePhyFileHeader.theSourcePhyCollisionText.Length > 0 Then
				line = ""
				theOutputFileStream.WriteLine(line)

				line = "$collisiontext"
				theOutputFileStream.WriteLine(line)

				line = "{"
				theOutputFileStream.WriteLine(line)

				Me.WriteTextLines(theSourceEngineModel.thePhyFileHeader.theSourcePhyCollisionText, 1)

				line = "}"
				theOutputFileStream.WriteLine(line)
			End If
		Catch ex As Exception

		End Try
	End Sub

	Private Sub WriteProceduralBonesCommand()
		'$proceduralbones "proceduralbones.vrd"
		If theSourceEngineModel.theMdlFileHeader.theProceduralBonesCommandIsUsed Then
			Dim line As String = ""
			line = "$proceduralbones "
			line += """"
			'line += "proceduralbones.vrd"
			line += theSourceEngineModel.GetVrdFileName()
			line += """"
			theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteBoneMergeCommand()
		Dim line As String = ""

		'$bonemerge "ValveBiped.Bip01_R_Hand"
		If theSourceEngineModel.theMdlFileHeader.theBones IsNot Nothing Then
			Dim aBone As SourceMdlBone
			Dim emptyLineIsAlreadyWritten As Boolean

			emptyLineIsAlreadyWritten = False
			For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theBones.Count - 1
				aBone = theSourceEngineModel.theMdlFileHeader.theBones(i)

				If (aBone.flags And SourceMdlBone.BONE_USED_BY_BONE_MERGE) > 0 Then
					If Not emptyLineIsAlreadyWritten Then
						line = ""
						theOutputFileStream.WriteLine(line)
						emptyLineIsAlreadyWritten = True
					End If

					line = "$bonemerge "
					line += """"
					line += aBone.theName
					line += """"
					theOutputFileStream.WriteLine(line)
				End If
			Next
		End If
	End Sub

	Private Sub WriteJiggleBoneCommand()
		If theSourceEngineModel.theMdlFileHeader.theBones Is Nothing Then
			Return
		End If

		Dim line As String = ""

		line = ""
		theOutputFileStream.WriteLine(line)

		For i As Integer = 0 To Me.theSourceEngineModel.theMdlFileHeader.theBones.Count - 1
			Dim aBone As SourceMdlBone
			aBone = Me.theSourceEngineModel.theMdlFileHeader.theBones(i)
			If aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_JIGGLE AndAlso aBone.proceduralRuleOffset <> 0 Then
				line = "$jigglebone "
				line += """"
				line += aBone.theName
				line += """"
				theOutputFileStream.WriteLine(line)
				line = "{"
				theOutputFileStream.WriteLine(line)
				If (aBone.theJiggleBone.flags And SourceMdlJiggleBone.JIGGLE_IS_FLEXIBLE) > 0 Then
					line = vbTab
					line += "is_flexible"
					theOutputFileStream.WriteLine(line)
					line = vbTab
					line += "{"
					theOutputFileStream.WriteLine(line)

					line = vbTab
					line += vbTab
					line += "length "
					line += aBone.theJiggleBone.length.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "tip_mass "
					line += aBone.theJiggleBone.tipMass.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)

					line = vbTab
					line += vbTab
					line += "pitch_stiffness "
					line += aBone.theJiggleBone.pitchStiffness.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "pitch_damping "
					line += aBone.theJiggleBone.pitchDamping.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "yaw_stiffness "
					line += aBone.theJiggleBone.yawStiffness.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "yaw_damping "
					line += aBone.theJiggleBone.yawDamping.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)

					If (aBone.theJiggleBone.flags And SourceMdlJiggleBone.JIGGLE_HAS_LENGTH_CONSTRAINT) > 0 Then
						line = vbTab
						line += vbTab
						line += "allow_length_flex"
						theOutputFileStream.WriteLine(line)
					End If
					line = vbTab
					line += vbTab
					line += "along_stiffness "
					line += aBone.theJiggleBone.alongStiffness.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "along_damping "
					line += aBone.theJiggleBone.alongDamping.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)

					Me.WriteJiggleBoneConstraints(aBone)

					line = vbTab
					line += "}"
					theOutputFileStream.WriteLine(line)
				End If
				If (aBone.theJiggleBone.flags And SourceMdlJiggleBone.JIGGLE_IS_RIGID) > 0 Then
					line = vbTab
					line += "is_rigid"
					theOutputFileStream.WriteLine(line)
					line = vbTab
					line += "{"
					theOutputFileStream.WriteLine(line)

					line = vbTab
					line += vbTab
					line += "length "
					line += aBone.theJiggleBone.length.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "tip_mass "
					line += aBone.theJiggleBone.tipMass.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)

					Me.WriteJiggleBoneConstraints(aBone)

					line = vbTab
					line += "}"
					theOutputFileStream.WriteLine(line)
				End If
				If (aBone.theJiggleBone.flags And SourceMdlJiggleBone.JIGGLE_HAS_BASE_SPRING) > 0 Then
					line = vbTab
					line += "has_base_spring"
					theOutputFileStream.WriteLine(line)
					line = vbTab
					line += "{"
					theOutputFileStream.WriteLine(line)

					line = vbTab
					line += vbTab
					line += "base_mass "
					line += aBone.theJiggleBone.baseMass.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "stiffness "
					line += aBone.theJiggleBone.baseStiffness.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "damping "
					line += aBone.theJiggleBone.baseDamping.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)

					line = vbTab
					line += vbTab
					line += "left_constraint "
					line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMinLeft).ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMaxLeft).ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "left_friction "
					line += aBone.theJiggleBone.baseLeftFriction.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)

					line = vbTab
					line += vbTab
					line += "up_constraint "
					line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMinUp).ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMaxUp).ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "up_friction "
					line += aBone.theJiggleBone.baseUpFriction.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)

					line = vbTab
					line += vbTab
					line += "forward_constraint "
					line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMinForward).ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMaxForward).ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "forward_friction "
					line += aBone.theJiggleBone.baseForwardFriction.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)

					line = vbTab
					line += "}"
					theOutputFileStream.WriteLine(line)
				End If
				line = "}"
				theOutputFileStream.WriteLine(line)
			End If
		Next
	End Sub

	Private Sub WriteJiggleBoneConstraints(ByVal aBone As SourceMdlBone)
		Dim line As String = ""

		If (aBone.theJiggleBone.flags And SourceMdlJiggleBone.JIGGLE_HAS_PITCH_CONSTRAINT) > 0 Then
			line = vbTab
			line += vbTab
			line += "pitch_constraint "
			line += MathModule.RadiansToDegrees(aBone.theJiggleBone.minPitch).ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += MathModule.RadiansToDegrees(aBone.theJiggleBone.maxPitch).ToString("0.######", TheApp.InternalNumberFormat)
			theOutputFileStream.WriteLine(line)
			line = vbTab
			line += vbTab
			line += "pitch_friction "
			line += aBone.theJiggleBone.pitchFriction.ToString("0.######", TheApp.InternalNumberFormat)
			theOutputFileStream.WriteLine(line)
			line = vbTab
			line += vbTab
			line += "pitch_bounce "
			line += aBone.theJiggleBone.pitchBounce.ToString("0.######", TheApp.InternalNumberFormat)
			theOutputFileStream.WriteLine(line)
		End If

		If (aBone.theJiggleBone.flags And SourceMdlJiggleBone.JIGGLE_HAS_YAW_CONSTRAINT) > 0 Then
			line = vbTab
			line += vbTab
			line += "yaw_constraint "
			line += MathModule.RadiansToDegrees(aBone.theJiggleBone.minYaw).ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += MathModule.RadiansToDegrees(aBone.theJiggleBone.maxYaw).ToString("0.######", TheApp.InternalNumberFormat)
			theOutputFileStream.WriteLine(line)
			line = vbTab
			line += vbTab
			line += "yaw_friction "
			line += aBone.theJiggleBone.yawFriction.ToString("0.######", TheApp.InternalNumberFormat)
			theOutputFileStream.WriteLine(line)
			line = vbTab
			line += vbTab
			line += "yaw_bounce "
			line += aBone.theJiggleBone.yawBounce.ToString("0.######", TheApp.InternalNumberFormat)
			theOutputFileStream.WriteLine(line)
		End If

		If (aBone.theJiggleBone.flags And SourceMdlJiggleBone.JIGGLE_HAS_ANGLE_CONSTRAINT) > 0 Then
			line = vbTab
			line += vbTab
			line += "angle_constraint "
			line += aBone.theJiggleBone.angleLimit.ToString("0.######", TheApp.InternalNumberFormat)
			theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteBBoxCommand()
		Dim line As String = ""
		Dim minX As Double
		Dim minY As Double
		Dim minZ As Double
		Dim maxX As Double
		Dim maxY As Double
		Dim maxZ As Double

		line = ""
		theOutputFileStream.WriteLine(line)

		'$bbox -16.0 -16.0 -13.0 16.0 16.0 75.0
		'FROM: VDC wiki: 
		'$bbox (min x) (min y) (min z) (max x) (max y) (max z)
		minX = Math.Round(theSourceEngineModel.theMdlFileHeader.hullMinPositionX, 3)
		minY = Math.Round(theSourceEngineModel.theMdlFileHeader.hullMinPositionY, 3)
		minZ = Math.Round(theSourceEngineModel.theMdlFileHeader.hullMinPositionZ, 3)
		maxX = Math.Round(theSourceEngineModel.theMdlFileHeader.hullMaxPositionX, 3)
		maxY = Math.Round(theSourceEngineModel.theMdlFileHeader.hullMaxPositionY, 3)
		maxZ = Math.Round(theSourceEngineModel.theMdlFileHeader.hullMaxPositionZ, 3)
		line = ""
		line += "// "
		line += "$bbox "
		line += minX.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += minY.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += minZ.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += maxX.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += maxY.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += maxZ.ToString("0.######", TheApp.InternalNumberFormat)
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteCBoxCommand()
		Dim line As String = ""
		Dim minX As Double
		Dim minY As Double
		Dim minZ As Double
		Dim maxX As Double
		Dim maxY As Double
		Dim maxZ As Double

		line = ""
		theOutputFileStream.WriteLine(line)

		'FROM: VDC wiki: 
		'$cbox <float|minx> <float|miny> <float|minz> <float|maxx> <float|maxy> <float|maxz> 
		minX = Math.Round(theSourceEngineModel.theMdlFileHeader.viewBoundingBoxMinPositionX, 3)
		minY = Math.Round(theSourceEngineModel.theMdlFileHeader.viewBoundingBoxMinPositionY, 3)
		minZ = Math.Round(theSourceEngineModel.theMdlFileHeader.viewBoundingBoxMinPositionZ, 3)
		maxX = Math.Round(theSourceEngineModel.theMdlFileHeader.viewBoundingBoxMaxPositionX, 3)
		maxY = Math.Round(theSourceEngineModel.theMdlFileHeader.viewBoundingBoxMaxPositionY, 3)
		maxZ = Math.Round(theSourceEngineModel.theMdlFileHeader.viewBoundingBoxMaxPositionZ, 3)
		line = "// $cbox is probably not used anymore"
		theOutputFileStream.WriteLine(line)
		line = "// $cbox "
		line += minX.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += minY.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += minZ.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += maxX.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += maxY.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += maxZ.ToString("0.######", TheApp.InternalNumberFormat)
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteHboxRelatedCommands()
		Dim line As String = ""
		Dim commentTag As String = ""
		Dim theSkipBoneInBBoxCommandWasUsed As Boolean = False

		line = ""
		theOutputFileStream.WriteLine(line)

		If (theSourceEngineModel.theMdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_AUTOGENERATED_HITBOX) > 0 Then
			If TheApp.Settings.DecompileQcFileExtraInfoIsChecked Then
				line = "// The hitbox info below was automatically generated when compiled because no hitbox info was provided."
				theOutputFileStream.WriteLine(line)
			End If

			commentTag = "// "

			If Not TheApp.Settings.DecompileQcFileExtraInfoIsChecked Then
				Exit Sub
			End If
		End If

		'FROM: HLMV for survivor_producer: 
		'$hboxset "L4D"
		'$hbox 3 "ValveBiped.Bip01_Pelvis"	    -5.33   -4.00   -4.00     5.33    4.00    4.00
		'$hbox 6 "ValveBiped.Bip01_L_Thigh"	     4.44   -3.02   -2.53    16.87    2.31    1.91
		'$hbox 6 "ValveBiped.Bip01_L_Calf"	     0.44   -1.78   -2.22    17.32    2.66    2.22
		'$hbox 6 "ValveBiped.Bip01_L_Toe0"	    -3.11   -0.44   -1.20     1.33    1.33    2.18
		'$hbox 7 "ValveBiped.Bip01_R_Thigh"	     4.44   -3.02   -2.53    16.87    2.31    1.91
		'$hbox 7 "ValveBiped.Bip01_R_Calf"	     0.44   -1.78   -2.22    17.32    2.66    2.22
		'$hbox 7 "ValveBiped.Bip01_R_Toe0"	    -3.11   -0.44   -1.20     1.33    1.33    2.18
		'$hbox 3 "ValveBiped.Bip01_Spine1"	    -4.44   -3.77   -5.33     4.44    5.55    5.33
		'$hbox 2 "ValveBiped.Bip01_Spine2"	    -2.66   -3.02   -5.77    10.66    5.86    5.77
		'$hbox 1 "ValveBiped.Bip01_Neck1"	     0.00   -2.22   -2.00     3.55    2.22    2.00
		'$hbox 1 "ValveBiped.Bip01_Head1"	    -0.71   -3.55   -2.71     6.39    3.55    2.18
		'$hbox 4 "ValveBiped.Bip01_L_UpperArm"	     0.00   -1.86   -1.78     9.77    1.69    1.78
		'$hbox 4 "ValveBiped.Bip01_L_Forearm"	     0.44   -1.55   -1.55    10.21    1.55    1.55
		'$hbox 4 "ValveBiped.Bip01_L_Hand"	     0.94   -1.28   -2.13     4.94    0.50    1.15
		'$hbox 5 "ValveBiped.Bip01_R_UpperArm"	     0.00   -1.86   -1.78     9.77    1.69    1.78
		'$hbox 5 "ValveBiped.Bip01_R_Forearm"	     0.44   -1.55   -1.55    10.21    1.55    1.55
		'$hbox 5 "ValveBiped.Bip01_R_Hand"	     0.94   -1.28   -2.13     4.94    0.50    1.15

		Dim aHitboxSet As SourceMdlHitboxSet
		Dim aHitbox As SourceMdlHitbox
		For i As Integer = 0 To Me.theSourceEngineModel.theMdlFileHeader.theHitboxSets.Count - 1
			aHitboxSet = Me.theSourceEngineModel.theMdlFileHeader.theHitboxSets(i)

			line = "$hboxset "
			line += """"
			line += aHitboxSet.theName
			line += """"
			theOutputFileStream.WriteLine(commentTag + line)

			If aHitboxSet.theHitboxes Is Nothing Then
				Continue For
			End If

			For j As Integer = 0 To aHitboxSet.theHitboxes.Count - 1
				aHitbox = aHitboxSet.theHitboxes(j)
				line = "$hbox "
				line += aHitbox.groupIndex.ToString(TheApp.InternalNumberFormat)
				line += " "
				line += """"
				line += Me.theSourceEngineModel.theMdlFileHeader.theBones(aHitbox.boneIndex).theName
				line += """"
				line += " "
				line += aHitbox.boundingBoxMinX.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aHitbox.boundingBoxMinY.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aHitbox.boundingBoxMinZ.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aHitbox.boundingBoxMaxX.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aHitbox.boundingBoxMaxY.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aHitbox.boundingBoxMaxZ.ToString("0.######", TheApp.InternalNumberFormat)
				theOutputFileStream.WriteLine(commentTag + line)

				If Not theSkipBoneInBBoxCommandWasUsed Then
					If aHitbox.boundingBoxMinX > 0 _
					 OrElse aHitbox.boundingBoxMinY > 0 _
					 OrElse aHitbox.boundingBoxMinZ > 0 _
					 OrElse aHitbox.boundingBoxMaxX < 0 _
					 OrElse aHitbox.boundingBoxMaxY < 0 _
					 OrElse aHitbox.boundingBoxMaxZ < 0 _
					 Then
						theSkipBoneInBBoxCommandWasUsed = True
					End If
				End If
			Next
		Next

		If theSkipBoneInBBoxCommandWasUsed Then
			line = "$skipboneinbbox"
			theOutputFileStream.WriteLine(commentTag + line)
		End If
	End Sub

	Private Sub WriteBodyGroupCommand(ByVal startIndex As Integer)
		Dim line As String = ""
		Dim aBodyPart As SourceMdlBodyPart
		Dim aModel As SourceMdlModel

		'$bodygroup "belt"
		'{
		'//	studio "zoey_belt.smd"
		'	"blank"
		'}
		'$bodygroup "shoes"
		'{
		'//  studio "zoey_shoes.smd"
		'    studio "zoey_feet.smd"
		'}
		'FROM: VDC wiki: 
		'$bodygroup sights
		'{
		'	studio "ironsights.smd"
		'	studio "laser_dot.smd"
		'	blank
		'}
		If theSourceEngineModel.theMdlFileHeader.theBodyParts IsNot Nothing AndAlso theSourceEngineModel.theMdlFileHeader.theBodyParts.Count > startIndex Then
			line = ""
			theOutputFileStream.WriteLine(line)

			'NOTE: Skip the first because $body(?) and $model handle that one.
			For bodyPartIndex As Integer = startIndex To theSourceEngineModel.theMdlFileHeader.theBodyParts.Count - 1
				aBodyPart = theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex)

				line = "$bodygroup "
				line += """"
				line += aBodyPart.theName
				line += """"
				theOutputFileStream.WriteLine(line)

				line = "{"
				theOutputFileStream.WriteLine(line)

				If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
					For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
						aModel = aBodyPart.theModels(modelIndex)

						line = vbTab
						If aModel.name(0) = ChrW(0) Then
							line += "blank"
						Else
							line += "studio "
							line += """"
							'line += Me.GetModelPathFileName(aModel)
							line += Me.theSourceEngineModel.GetBodyGroupSmdFileName(bodyPartIndex, modelIndex, 0)
							line += """"
						End If
						theOutputFileStream.WriteLine(line)
					Next
				End If

				line = "}"
				theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	Private Sub WriteControllerCommand()
		Dim line As String = ""
		Dim boneController As SourceMdlBoneController

		'$controller mouth "jaw" X 0 20
		'$controller 0 "tracker" LYR -1 1
		Try
			If theSourceEngineModel.theMdlFileHeader.theBoneControllers IsNot Nothing Then
				line = ""
				theOutputFileStream.WriteLine(line)

				For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theBoneControllers.Count - 1
					boneController = theSourceEngineModel.theMdlFileHeader.theBoneControllers(i)

					line = "$controller "
					line += boneController.inputField.ToString(TheApp.InternalNumberFormat)
					line += " """
					line += theSourceEngineModel.theMdlFileHeader.theBones(boneController.boneIndex).theName
					line += """ "
					line += boneController.TypeName
					line += boneController.startBlah.ToString("0.######", TheApp.InternalNumberFormat)
					line += boneController.endBlah.ToString("0.######", TheApp.InternalNumberFormat)
					theOutputFileStream.WriteLine(line)
				Next
			End If
		Catch ex As Exception

		End Try
	End Sub

	Private Sub WriteScreenAlignCommand()
		Dim line As String = ""
		Dim aBone As SourceMdlBone

		'$screenalign <bone name> <"sphere" or "cylinder">
		Try
			If theSourceEngineModel.theMdlFileHeader.theBones IsNot Nothing Then
				line = ""
				theOutputFileStream.WriteLine(line)

				For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theBones.Count - 1
					aBone = theSourceEngineModel.theMdlFileHeader.theBones(i)

					If (aBone.flags And SourceMdlBone.BONE_SCREEN_ALIGN_SPHERE) > 0 Then
						line = "$screenalign "
						line += aBone.theName
						line += " ""sphere"""
						theOutputFileStream.WriteLine(line)
					ElseIf (aBone.flags And SourceMdlBone.BONE_SCREEN_ALIGN_CYLINDER) > 0 Then
						line = "$screenalign "
						line += aBone.theName
						line += " ""cylinder"""
						theOutputFileStream.WriteLine(line)
					End If
				Next
			End If
		Catch ex As Exception

		End Try
	End Sub

	Private Sub WriteKeyValues(ByVal keyValuesText As String, ByVal commandOrOptionText As String)
		Dim line As String = ""
		Dim startText As String = "mdlkeyvalue" + vbLf
		Dim text As String

		'$keyvalues
		'{
		'	"particles"
		'	{
		'		"effect"
		'		{
		'		name("sparks_head")
		'		attachment_type("follow_attachment")
		'		attachment_point("Head_sparks")
		'		}
		'		"effect"
		'		{
		'		name("sparks_head_wire1")
		'		attachment_type("follow_attachment")
		'		attachment_point("Head_Wire_1")
		'		}
		'		"effect"
		'		{
		'		name("sparks_knee_wire1")
		'		attachment_type("follow_attachment")
		'		attachment_point("R_Knee_Wire_1")
		'		}
		'		"effect"
		'		{
		'		name("sparks_knee_wire2")
		'		attachment_type("follow_attachment")
		'		attachment_point("R_Knee_Wire_2")
		'		}
		'		"effect"
		'		{
		'		name("sparks_ankle_wire1")
		'		attachment_type("follow_attachment")
		'		attachment_point("L_Ankle_Wire_1")
		'		}
		'		"effect"
		'		{
		'		name("sparks_ankle_wire2")
		'		attachment_type("follow_attachment")
		'		attachment_point("L_Ankle_Wire_2")
		'		}			
		'	}
		'}
		Try
			If keyValuesText IsNot Nothing AndAlso keyValuesText.Length > 0 Then
				line = ""
				theOutputFileStream.WriteLine(line)

				line = commandOrOptionText
				theOutputFileStream.WriteLine(line)

				If keyValuesText.StartsWith(startText) Then
					text = keyValuesText.Remove(0, startText.Length)
				Else
					text = keyValuesText
				End If

				'lengthToRemove = 0
				'While True
				'	stopIndex = text.IndexOf(openBraceText)
				'	If stopIndex > -1 Then
				'		If stopIndex > 0 Then
				'			line = text.Substring(0, stopIndex)
				'			theOutputFileStream.WriteLine(line)
				'		End If

				'		line = "{"
				'		lengthToRemove = stopIndex + openBraceText.Length
				'	Else
				'		stopIndex = text.IndexOf(closeBraceText)
				'		If stopIndex > -1 Then
				'			If stopIndex > 0 Then
				'				line = text.Substring(0, stopIndex)
				'				theOutputFileStream.WriteLine(line)
				'			End If

				'			line = "}"
				'			lengthToRemove = stopIndex + closeBraceText.Length
				'		Else
				'			stopIndex = text.IndexOf(linefeedCharText)
				'			If stopIndex > -1 Then
				'				line = text.Substring(0, stopIndex)
				'				lengthToRemove = stopIndex + linefeedCharText.Length
				'			Else
				'				line = text
				'			End If
				'		End If
				'	End If
				'	theOutputFileStream.WriteLine(line)

				'	If stopIndex > -1 Then
				'		text = text.Remove(0, lengthToRemove)
				'		If text = "" Then
				'			Exit While
				'		End If
				'	End If
				'End While

				Me.WriteTextLines(text, 0)
			End If
		Catch ex As Exception

		End Try
	End Sub

	Private Sub WriteTextLines(ByVal text As String, ByVal indentCount As Integer)
		Dim line As String = ""
		Dim textChar As Char
		Dim startIndex As Integer
		Dim indentText As String
		Dim lineQuoteCount As Integer
		Dim lineWordCount As Integer
		Dim beforeCloseBraceText As String

		indentText = ""
		For j As Integer = 1 To indentCount
			indentText += vbTab
		Next

		startIndex = 0
		lineQuoteCount = 0
		lineWordCount = 0
		For i As Integer = 0 To text.Length - 1
			textChar = text(i)
			If textChar = "{" Then
				If i > startIndex Then
					line = indentText
					line += text.Substring(startIndex, i - startIndex)
					theOutputFileStream.WriteLine(line)
				End If

				line = indentText
				line += "{"
				theOutputFileStream.WriteLine(line)

				indentCount += 1
				indentText = ""
				For j As Integer = 1 To indentCount
					indentText += vbTab
				Next

				startIndex = i + 1
				lineQuoteCount = 0
			ElseIf textChar = "}" Then
				If i > startIndex Then
					beforeCloseBraceText = text.Substring(startIndex, i - startIndex).Trim()
					If beforeCloseBraceText <> "" Then
						line = indentText
						line += beforeCloseBraceText
						theOutputFileStream.WriteLine(line)
					End If
				End If

				indentCount -= 1
				indentText = ""
				For j As Integer = 1 To indentCount
					indentText += vbTab
				Next

				line = indentText
				line += "}"
				theOutputFileStream.WriteLine(line)

				startIndex = i + 1
				lineQuoteCount = 0
			ElseIf textChar = """" Then
				lineQuoteCount += 1
				If lineQuoteCount = 4 Then
					If i > startIndex Then
						line = indentText
						line += text.Substring(startIndex, i - startIndex + 1).Trim()
						theOutputFileStream.WriteLine(line)
					End If
					startIndex = i + 1
					lineQuoteCount = 0
				End If
				'If lineQuoteCount = 2 OrElse lineQuoteCount = 4 Then
				'	lineWordCount += 1
				'End If
			ElseIf textChar = vbLf Then
				startIndex = i + 1
				lineQuoteCount = 0
			End If
		Next
	End Sub

#End Region

#Region "Data"

	Private theSourceEngineModel As SourceModel
	Private theOutputFileStream As StreamWriter

	Private theFirstAnimationDescName As String

#End Region

End Class
