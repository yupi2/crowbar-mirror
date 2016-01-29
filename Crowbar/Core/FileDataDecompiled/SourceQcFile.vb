Imports System.IO
Imports System.Text

Public Class SourceQcFile

#Region "Methods"

	Public Sub WriteFile(ByVal outputPathFileName As String, ByVal aSourceEngineModel As SourceModel)
		Me.theSourceEngineModel = aSourceEngineModel

		Me.theOutputPathName = FileManager.GetPath(outputPathFileName)
		Me.theOutputFileNameWithoutExtension = Path.GetFileNameWithoutExtension(outputPathFileName)

		Me.WriteQcFile(outputPathFileName)
	End Sub

	Public Function GetMdlRelativePathFileName(ByVal qcPathFileName As String) As String
		Dim modelRelativePathFileName As String

		modelRelativePathFileName = ""

		Using inputFileStream As StreamReader = New StreamReader(qcPathFileName)
			Dim inputLine As String
			Dim temp As String

			While (Not (inputFileStream.EndOfStream))
				inputLine = inputFileStream.ReadLine()

				temp = inputLine.ToLower().TrimStart()
				If temp.StartsWith("$modelname") Then
					temp = temp.Replace("$modelname", "")
					temp = temp.Trim()
					temp = temp.Trim(Chr(34))
					modelRelativePathFileName = temp.Replace("/", "\")
					Exit While
				End If
			End While
		End Using

		Return modelRelativePathFileName
	End Function

	Public Sub InsertAnIncludeFileCommand(ByVal qcPathFileName As String, ByVal includedPathFileName As String)
		Using outputFileStream As StreamWriter = File.AppendText(qcPathFileName)
			If File.Exists(includedPathFileName) Then
				Dim qciFileInfo As New FileInfo(includedPathFileName)
				If qciFileInfo.Length > 0 Then
					Dim line As String = ""

					outputFileStream.WriteLine()

					line += "$Include"
					line += " "
					line += """"
					line += FileManager.GetRelativePath(qcPathFileName, includedPathFileName)
					line += """"
					outputFileStream.WriteLine(line)
				End If
			End If
		End Using
	End Sub

#End Region

#Region "Private Delegates"

	Private Delegate Sub WriteGroupDelegate()

#End Region

#Region "Private Methods"

	Private Sub WriteQcFile(ByVal outputPathFileName As String)
		Try
			Me.theOutputFileStream = File.CreateText(outputPathFileName)

			Me.WriteHeaderComment()

			Me.WriteModelNameCommand()

			Me.WriteStaticPropCommand()
			Me.WriteConstDirectionalLightCommand()

			If Me.theSourceEngineModel.MdlFileHeader.theModelCommandIsUsed Then
				Me.WriteModelCommand()
				Me.WriteBodyGroupCommand(1)
			Else
				Me.WriteBodyGroupCommand(0)
			End If
			Me.WriteGroup("lod", AddressOf WriteGroupLod, False, False)

			Me.WriteSurfacePropCommand()
			Me.WriteJointSurfacePropCommand()
			Me.WriteContentsCommand()
			Me.WriteJointContentsCommand()
			If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
				Me.WriteIllumPositionCommand()
			End If

			Me.WriteEyePositionCommand()
			Me.WriteMaxEyeDeflectionCommand()
			Me.WriteNoForcedFadeCommand()
			Me.WriteForcePhonemeCrossfadeCommand()

			Me.WriteAmbientBoostCommand()
			Me.WriteOpaqueCommand()
			Me.WriteObsoleteCommand()
			Me.WriteCdMaterialsCommand()
			Me.WriteTextureGroupCommand()
			If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
				Me.WriteTextureFileNameComments()
			End If

			Me.WriteAttachmentCommand()

			Me.WriteGroup("box", AddressOf WriteGroupBox, True, False)

			Me.WriteControllerCommand()
			Me.WriteScreenAlignCommand()

			Me.WriteGroup("bone", AddressOf WriteGroupBone, False, False)

			Me.WriteGroup("animation", AddressOf WriteGroupAnimation, False, False)

			Me.WriteGroup("collision", AddressOf WriteGroupCollision, False, False)

			Me.WriteKeyValues(theSourceEngineModel.MdlFileHeader.theKeyValuesText, "$KeyValues")
		Catch ex As Exception
		Finally
			If Me.theOutputFileStream IsNot Nothing Then
				Me.theOutputFileStream.Flush()
				Me.theOutputFileStream.Close()
			End If
		End Try
	End Sub

	Private Sub WriteGroup(ByVal qciGroupName As String, ByVal writeGroupAction As WriteGroupDelegate, ByVal includeLineIsCommented As Boolean, ByVal includeLineIsIndented As Boolean)
		If TheApp.Settings.DecompileGroupIntoQciFilesIsChecked Then
			Dim qciFileName As String
			Dim qciPathFileName As String
			Dim mainOutputFileStream As StreamWriter

			mainOutputFileStream = Me.theOutputFileStream

			Try
				'qciPathFileName = Path.Combine(Me.theOutputPathName, Me.theOutputFileNameWithoutExtension + "_flexes.qci")
				qciFileName = Me.theOutputFileNameWithoutExtension + "_" + qciGroupName + ".qci"
				qciPathFileName = Path.Combine(Me.theOutputPathName, qciFileName)

				Me.theOutputFileStream = File.CreateText(qciPathFileName)

				'Me.WriteFlexLines()
				'Me.WriteFlexControllerLines()
				'Me.WriteFlexRuleLines()
				writeGroupAction.Invoke()
			Catch ex As Exception
				Throw
			Finally
				If Me.theOutputFileStream IsNot Nothing Then
					Me.theOutputFileStream.Flush()
					Me.theOutputFileStream.Close()

					Me.theOutputFileStream = mainOutputFileStream
				End If
			End Try

			Try
				If File.Exists(qciPathFileName) Then
					Dim qciFileInfo As New FileInfo(qciPathFileName)
					If qciFileInfo.Length > 0 Then
						Dim line As String = ""

						Me.theOutputFileStream.WriteLine()

						If includeLineIsCommented Then
							line += "// "
						End If
						If includeLineIsIndented Then
							line += vbTab
						End If
						line += "$Include"
						line += " "
						line += """"
						line += qciFileName
						line += """"
						Me.theOutputFileStream.WriteLine(line)
					End If
				End If
			Catch ex As Exception
				Throw
			End Try
		Else
			'Me.WriteFlexLines()
			'Me.WriteFlexControllerLines()
			'Me.WriteFlexRuleLines()
			writeGroupAction.Invoke()
		End If
	End Sub

	Private Sub WriteModelNameCommand()
		Dim line As String = ""
		'Dim modelPath As String
		Dim modelPathFileName As String

		'modelPath = FileManager.GetPath(CStr(theSourceEngineModel.theMdlFileHeader.name).Trim(Chr(0)))
		'modelPathFileName = Path.Combine(modelPath, theSourceEngineModel.ModelName + ".mdl")
		'modelPathFileName = CStr(theSourceEngineModel.MdlFileHeader.name).Trim(Chr(0))
		modelPathFileName = theSourceEngineModel.MdlFileHeader.theName

		Me.theOutputFileStream.WriteLine()

		'$modelname "survivors/survivor_producer.mdl"
		'$modelname "custom/survivor_producer.mdl"
		line = "$ModelName "
		line += """"
		line += modelPathFileName
		line += """"
		Me.theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteIncludeMainQcLine()
		Dim line As String = ""

		Me.theOutputFileStream.WriteLine()

		'$include "Rochelle_world.qci"
		line = "$Include "
		line += """"
		line += "decompiled.qci"
		line += """"
		Me.theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteHeaderComment()
		Dim line As String = ""

		line = "// "
		line += TheApp.GetHeaderComment()
		Me.theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteStaticPropCommand()
		Dim line As String = ""

		'$staticprop
		If (theSourceEngineModel.MdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
			Me.theOutputFileStream.WriteLine()

			line = "$StaticProp"
			Me.theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteConstDirectionalLightCommand()
		Dim line As String = ""

		'$constantdirectionallight
		If (theSourceEngineModel.MdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_CONSTANT_DIRECTIONAL_LIGHT_DOT) > 0 Then
			Me.theOutputFileStream.WriteLine()

			line = "$ConstantDirectionalLight "
			'FROM: studiomdl.cpp
			'g_constdirectionalightdot = (byte)( verify_atof(token) * 255.0f );
			line += CStr(Me.theSourceEngineModel.MdlFileHeader.directionalLightDot / 255)
			Me.theOutputFileStream.WriteLine(line)
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
		If theSourceEngineModel.MdlFileHeader.theBodyParts IsNot Nothing AndAlso theSourceEngineModel.MdlFileHeader.theBodyParts.Count > 0 Then
			line = ""
			Me.theOutputFileStream.WriteLine(line)

			'referenceSmdFileName = Me.GetModelPathFileName(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0))
			'referenceSmdFileName = theSourceEngineModel.GetLodSmdFileName(0)
			referenceSmdFileName = Me.theSourceEngineModel.GetBodyGroupSmdFileName(0, 0, 0)

			line = "$Model "
			line += """"
			line += theSourceEngineModel.MdlFileHeader.theBodyParts(0).theName
			line += """ """
			line += referenceSmdFileName
			line += """"

			line += " {"
			Me.theOutputFileStream.WriteLine(line)

			'NOTE: Must call WriteEyeballLines() before WriteEyelidLines(), because eyeballNames are created in first and sent to other.
			Me.WriteEyeballLines(eyeballNames)
			Me.WriteEyelidLines(eyeballNames)

			Me.WriteMouthLines()

			Me.WriteGroup("flex", AddressOf WriteGroupFlex, False, True)

			line = "}"
			Me.theOutputFileStream.WriteLine(line)
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
			aBodyPart = theSourceEngineModel.MdlFileHeader.theBodyParts(0)
			If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
				aModel = aBodyPart.theModels(0)
				If aModel.theEyeballs IsNot Nothing AndAlso aModel.theEyeballs.Count > 0 Then
					line = ""
					Me.theOutputFileStream.WriteLine(line)

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
						aBone = Me.theSourceEngineModel.MdlFileHeader.theBones(anEyeball.boneIndex)
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
							eyeballTextureName = theSourceEngineModel.MdlFileHeader.theTextures(anEyeball.theTextureIndex).theName
							'eyeballTextureName = Path.GetFileName(theSourceEngineModel.theMdlFileHeader.theTextures(anEyeball.theTextureIndex).theName)
						End If

						line = vbTab
						line += "eyeball """
						line += eyeballNames(eyeballIndex)
						line += """ """
						line += theSourceEngineModel.MdlFileHeader.theBones(anEyeball.boneIndex).theName
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
						Me.theOutputFileStream.WriteLine(line)

						'NOTE: Used to write frame indexes for eyelid lines and prevent eyelid flexes from being written in flex list in qc file.
						theSourceEngineModel.MdlFileHeader.theFlexDescs(anEyeball.upperLidFlexDesc).theDescIsUsedByEyelid = True
						theSourceEngineModel.MdlFileHeader.theFlexDescs(anEyeball.lowerLidFlexDesc).theDescIsUsedByEyelid = True
					Next
				End If
			End If
		Catch
		End Try

		Me.CreateListOfEyelidFlexFrameIndexes()
	End Sub

	Private Sub CreateListOfEyelidFlexFrameIndexes()
		Dim aFlexFrame As FlexFrame

		theSourceEngineModel.MdlFileHeader.theEyelidFlexFrameIndexes = New List(Of Integer)()
		For frameIndex As Integer = 1 To theSourceEngineModel.MdlFileHeader.theFlexFrames.Count - 1
			aFlexFrame = theSourceEngineModel.MdlFileHeader.theFlexFrames(frameIndex)
			If Not theSourceEngineModel.MdlFileHeader.theEyelidFlexFrameIndexes.Contains(frameIndex) Then
				If Me.theSourceEngineModel.MdlFileHeader.theFlexDescs(aFlexFrame.flexes(0).flexDescIndex).theDescIsUsedByEyelid Then
					theSourceEngineModel.MdlFileHeader.theEyelidFlexFrameIndexes.Add(frameIndex)
				End If
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
			aBodyPart = theSourceEngineModel.MdlFileHeader.theBodyParts(0)
			If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 AndAlso theSourceEngineModel.MdlFileHeader.theEyelidFlexFrameIndexes.Count > 0 Then
				aModel = aBodyPart.theModels(0)
				If aModel.theEyeballs IsNot Nothing AndAlso aModel.theEyeballs.Count > 0 Then
					line = ""
					Me.theOutputFileStream.WriteLine(line)

					frameIndex = 0
					For eyeballIndex As Integer = 0 To aModel.theEyeballs.Count - 1
						anEyeball = aModel.theEyeballs(eyeballIndex)

						If frameIndex + 3 >= theSourceEngineModel.MdlFileHeader.theEyelidFlexFrameIndexes.Count Then
							frameIndex = 0
						End If
						eyelidName = theSourceEngineModel.MdlFileHeader.theFlexDescs(anEyeball.upperLidFlexDesc).theName

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
						line += theSourceEngineModel.MdlFileHeader.theEyelidFlexFrameIndexes(frameIndex).ToString(TheApp.InternalNumberFormat)
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
						line += theSourceEngineModel.MdlFileHeader.theEyelidFlexFrameIndexes(frameIndex).ToString(TheApp.InternalNumberFormat)
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
						Me.theOutputFileStream.WriteLine(line)

						theSourceEngineModel.MdlFileHeader.theFlexDescs(anEyeball.upperLidFlexDesc).theDescIsUsedByFlex = True
						theSourceEngineModel.MdlFileHeader.theFlexDescs(anEyeball.upperFlexDesc(0)).theDescIsUsedByFlex = True
						theSourceEngineModel.MdlFileHeader.theFlexDescs(anEyeball.upperFlexDesc(1)).theDescIsUsedByFlex = True
						theSourceEngineModel.MdlFileHeader.theFlexDescs(anEyeball.upperFlexDesc(2)).theDescIsUsedByFlex = True

						eyelidName = theSourceEngineModel.MdlFileHeader.theFlexDescs(anEyeball.lowerLidFlexDesc).theName

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
						line += theSourceEngineModel.MdlFileHeader.theEyelidFlexFrameIndexes(frameIndex).ToString(TheApp.InternalNumberFormat)
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
						line += theSourceEngineModel.MdlFileHeader.theEyelidFlexFrameIndexes(frameIndex).ToString(TheApp.InternalNumberFormat)
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
						Me.theOutputFileStream.WriteLine(line)

						theSourceEngineModel.MdlFileHeader.theFlexDescs(anEyeball.lowerLidFlexDesc).theDescIsUsedByFlex = True
						theSourceEngineModel.MdlFileHeader.theFlexDescs(anEyeball.lowerFlexDesc(0)).theDescIsUsedByFlex = True
						theSourceEngineModel.MdlFileHeader.theFlexDescs(anEyeball.lowerFlexDesc(1)).theDescIsUsedByFlex = True
						theSourceEngineModel.MdlFileHeader.theFlexDescs(anEyeball.lowerFlexDesc(2)).theDescIsUsedByFlex = True
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
		If theSourceEngineModel.MdlFileHeader.theMouths IsNot Nothing AndAlso theSourceEngineModel.MdlFileHeader.theMouths.Count > 0 Then
			line = ""
			Me.theOutputFileStream.WriteLine(line)

			For i As Integer = 0 To theSourceEngineModel.MdlFileHeader.theMouths.Count - 1
				Dim aMouth As SourceMdlMouth
				aMouth = theSourceEngineModel.MdlFileHeader.theMouths(i)
				offsetX = Math.Round(aMouth.forwardX, 3)
				offsetY = Math.Round(aMouth.forwardY, 3)
				offsetZ = Math.Round(aMouth.forwardZ, 3)

				line = vbTab
				line += "mouth "
				line += i.ToString(TheApp.InternalNumberFormat)
				line += " """
				line += theSourceEngineModel.MdlFileHeader.theFlexDescs(aMouth.flexDescIndex).theName
				line += """ """
				line += theSourceEngineModel.MdlFileHeader.theBones(aMouth.boneIndex).theName
				line += """ "
				line += offsetX.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += offsetY.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += offsetZ.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStream.WriteLine(line)

				theSourceEngineModel.MdlFileHeader.theFlexDescs(aMouth.flexDescIndex).theDescIsUsedByFlex = True
			Next
		End If
	End Sub

	Private Sub WriteGroupFlex()
		Me.WriteFlexLines()
		Me.WriteFlexControllerLines()
		Me.WriteFlexRuleLines()
	End Sub

	Private Sub WriteFlexLines()
		Dim line As String = ""

		' Write flexfile (contains flexDescs).
		If theSourceEngineModel.MdlFileHeader.theFlexFrames IsNot Nothing AndAlso theSourceEngineModel.MdlFileHeader.theFlexFrames.Count > 0 Then
			line = ""
			Me.theOutputFileStream.WriteLine(line)

			line = vbTab
			line += "flexfile"
			'line += Path.GetFileNameWithoutExtension(CStr(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0).name).Trim(Chr(0)))
			'line += ".vta"""
			line += " """
			line += theSourceEngineModel.GetVtaFileName()
			line += """ "
			Me.theOutputFileStream.WriteLine(line)

			line = vbTab
			line += "{"
			Me.theOutputFileStream.WriteLine(line)

			'======
			line = vbTab
			line += vbTab
			line += "defaultflex frame 0"
			Me.theOutputFileStream.WriteLine(line)

			'NOTE: Start at index 1 because defaultflex frame is at index 0.
			Dim aFlexFrame As FlexFrame
			For frameIndex As Integer = 1 To theSourceEngineModel.MdlFileHeader.theFlexFrames.Count - 1
				aFlexFrame = theSourceEngineModel.MdlFileHeader.theFlexFrames(frameIndex)
				line = vbTab
				line += vbTab
				If Me.theSourceEngineModel.MdlFileHeader.theFlexDescs(aFlexFrame.flexes(0).flexDescIndex).theDescIsUsedByEyelid Then
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
				Me.theOutputFileStream.WriteLine(line)
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
			'Me.theOutputFileStream.WriteLine(line)

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
			'							Me.theOutputFileStream.WriteLine(line)
			'						Next
			'					End If
			'				Next
			'			End If
			'		Next
			'	End If
			'Next

			line = vbTab
			line += "}"
			Me.theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteFlexControllerLines()
		Dim line As String = ""

		'NOTE: Writes out flexcontrollers correctly for teenangst zoey.
		If theSourceEngineModel.MdlFileHeader.theFlexControllers IsNot Nothing AndAlso theSourceEngineModel.MdlFileHeader.theFlexControllers.Count > 0 Then
			Dim aFlexController As SourceMdlFlexController

			line = ""
			Me.theOutputFileStream.WriteLine(line)

			For i As Integer = 0 To theSourceEngineModel.MdlFileHeader.theFlexControllers.Count - 1
				aFlexController = theSourceEngineModel.MdlFileHeader.theFlexControllers(i)

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
				Me.theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	Private Sub WriteFlexRuleLines()
		Dim line As String = ""

		'NOTE: All flex rules are correct for teenangst zoey.
		If theSourceEngineModel.MdlFileHeader.theFlexRules IsNot Nothing AndAlso theSourceEngineModel.MdlFileHeader.theFlexRules.Count > 0 Then
			Dim aFlexRule As SourceMdlFlexRule

			line = ""
			Me.theOutputFileStream.WriteLine(line)

			For i As Integer = 0 To theSourceEngineModel.MdlFileHeader.theFlexDescs.Count - 1
				Dim flexDesc As SourceMdlFlexDesc
				flexDesc = theSourceEngineModel.MdlFileHeader.theFlexDescs(i)

				If Not flexDesc.theDescIsUsedByFlex AndAlso flexDesc.theDescIsUsedByFlexRule Then
					line = vbTab
					line += "localvar "
					line += flexDesc.theName
					Me.theOutputFileStream.WriteLine(line)
				End If
			Next

			For i As Integer = 0 To theSourceEngineModel.MdlFileHeader.theFlexRules.Count - 1
				aFlexRule = theSourceEngineModel.MdlFileHeader.theFlexRules(i)
				line = Me.GetFlexRule(aFlexRule)
				Me.theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	'#define clamp(val, min, max) (((val) > (max)) ? (max) : (((val) < (min)) ? (min) : (val)))
	Private Function Clamp(ByVal val As Double, ByVal min As Double, ByVal max As Double) As Double
		If val > max Then
			Return max
		ElseIf val < min Then
			Return min
		Else
			Return val
		End If
	End Function

	'inline float RemapValClamped( float val, float A, float B, float C, float D)
	'{
	'	if ( A == B )
	'		return val >= B ? D : C;
	'	float cVal = (val - A) / (B - A);
	'	cVal = clamp( cVal, 0.0f, 1.0f );

	'	return C + (D - C) * cVal;
	'}
	Private Function RemapValClamped(ByVal val As Double, ByVal A As Double, ByVal B As Double, ByVal C As Double, ByVal D As Double) As Double
		If A = B Then
			Return 0
		End If

		Dim cVal As Double
		cVal = (val - A) / (B - A)
		cVal = clamp(cVal, 0.0F, 1.0F)

		Return C + (D - C) * cVal
	End Function

	Private Function GetFlexRule(ByVal aFlexRule As SourceMdlFlexRule) As String
		Dim flexRuleEquation As String
		flexRuleEquation = vbTab
		flexRuleEquation += "%"
		flexRuleEquation += theSourceEngineModel.MdlFileHeader.theFlexDescs(aFlexRule.flexIndex).theName
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
					'int m = pFlexcontroller( (LocalFlexController_t)pops->d.index)->localToGlobal;
					'stack[k] = src[m];
					'k++; 
					stack.Push(New IntermediateExpression(theSourceEngineModel.MdlFileHeader.theFlexControllers(aFlexOp.index).theName, 10))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_FETCH2 Then
					stack.Push(New IntermediateExpression("%" + theSourceEngineModel.MdlFileHeader.theFlexDescs(aFlexOp.index).theName, 10))
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
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_EXP Then
					Dim ignoreThisOpBecauseItIsMistakeToBeHere As Integer = 4242
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_OPEN Then
					Dim ignoreThisOpBecauseItIsMistakeToBeHere As Integer = 4242
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_CLOSE Then
					Dim ignoreThisOpBecauseItIsMistakeToBeHere As Integer = 4242
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_COMMA Then
					Dim ignoreThisOpBecauseItIsMistakeToBeHere As Integer = 4242
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
					'TODO: SourceMdlFlexOp.STUDIO_2WAY_0
					'ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_2WAY_0 Then
					'	'	'#define STUDIO_2WAY_0	15	// Fetch a value from a 2 Way slider for the 1st value RemapVal( 0.0, 0.5, 0.0, 1.0 )
					'	'	'int m = pFlexcontroller( (LocalFlexController_t)pops->d.index )->localToGlobal;
					'	'	'stack[ k ] = RemapValClamped( src[m], -1.0f, 0.0f, 1.0f, 0.0f );
					'	'	'k++; 
					'	Dim newExpression As String
					'	'newExpression = CStr(Me.RemapValClamped(aFlexOp.value, -1, 0, 1, 0))
					'	newExpression = "RemapValClamped(" + theSourceEngineModel.theMdlFileHeader.theFlexControllers(aFlexOp.index).theName + ", -1, 0, 1, 0)"
					'	stack.Push(New IntermediateExpression(newExpression, 5))
					'TODO: SourceMdlFlexOp.STUDIO_2WAY_1
					'ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_2WAY_1 Then
					'	'#define STUDIO_2WAY_1	16	// Fetch a value from a 2 Way slider for the 2nd value RemapVal( 0.5, 1.0, 0.0, 1.0 )
					'	'int m = pFlexcontroller( (LocalFlexController_t)pops->d.index )->localToGlobal;
					'	'stack[ k ] = RemapValClamped( src[m], 0.0f, 1.0f, 0.0f, 1.0f );
					'	'k++; 
					'	Dim newExpression As String
					'	'newExpression = CStr(Me.RemapValClamped(aFlexOp.value, 0, 1, 0, 1))
					'	newExpression = "RemapValClamped(" + theSourceEngineModel.theMdlFileHeader.theFlexControllers(aFlexOp.index).theName + ", 0, 1, 0, 1)"
					'	stack.Push(New IntermediateExpression(newExpression, 5))
					'TODO: SourceMdlFlexOp.STUDIO_NWAY
					'ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_NWAY Then
					'	Dim x As Integer = 42
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_COMBO Then
					'#define STUDIO_COMBO	18	// Perform a combo operation (essentially multiply the last N values on the stack)
					'int m = pops->d.index;
					'int km = k - m;
					'for ( int i = km + 1; i < k; ++i )
					'{
					'	stack[ km ] *= stack[ i ];
					'}
					'k = k - m + 1;
					Dim count As Integer
					Dim newExpression As String
					Dim intermediateExp As IntermediateExpression
					count = aFlexOp.index
					newExpression = ""
					intermediateExp = stack.Pop()
					newExpression += intermediateExp.theExpression
					For j As Integer = 2 To count
						intermediateExp = stack.Pop()
						newExpression += " * " + intermediateExp.theExpression
					Next
					newExpression = "(" + newExpression + ")"
					stack.Push(New IntermediateExpression(newExpression, 5))
				ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_DOMINATE Then
					'int m = pops->d.index;
					'int km = k - m;
					'float dv = stack[ km ];
					'for ( int i = km + 1; i < k; ++i )
					'{
					'	dv *= stack[ i ];
					'}
					'stack[ km - 1 ] *= 1.0f - dv;
					'k -= m;
					Dim count As Integer
					Dim newExpression As String
					Dim intermediateExp As IntermediateExpression
					count = aFlexOp.index
					newExpression = ""
					intermediateExp = stack.Pop()
					newExpression += intermediateExp.theExpression
					For j As Integer = 2 To count
						intermediateExp = stack.Pop()
						newExpression += " * " + intermediateExp.theExpression
					Next
					intermediateExp = stack.Pop()
					newExpression = intermediateExp.theExpression + " * (1 - " + newExpression + ")"
					newExpression = "(" + newExpression + ")"
					stack.Push(New IntermediateExpression(newExpression, 5))
					'TODO: SourceMdlFlexOp.STUDIO_DME_LOWER_EYELID
					'ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_DME_LOWER_EYELID Then
					'	Dim x As Integer = 42
					'TODO: SourceMdlFlexOp.STUDIO_DME_UPPER_EYELID
					'ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_DME_UPPER_EYELID Then
					'	Dim x As Integer = 42
				Else
					stack.Clear()
					Exit For
				End If
			Next

			' The loop above leaves the final expression on the top of the stack.
			If stack.Count = 1 Then
				flexRuleEquation += stack.Peek().theExpression
			ElseIf stack.Count = 0 OrElse stack.Count > 1 Then
				flexRuleEquation = "// [Decompiler failed to parse expression. Please report the error with the following info: this qc file, the mdl filename that was decompiled, and where the mdl file was found (e.g. the game's name or a web link).]"
			Else
				flexRuleEquation = "// [Empty flex rule found and ignored.]"
			End If
		End If
		Return flexRuleEquation
	End Function

	Private Sub WriteGroupLod()
		Me.WriteLodCommand()
	End Sub

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
		If theSourceEngineModel.VtxFileHeader IsNot Nothing AndAlso Me.theSourceEngineModel.MdlFileHeader.theBodyParts IsNot Nothing Then
			'Dim referenceSmdFileName As String
			'Dim lodSmdFileName As String

			If theSourceEngineModel.VtxFileHeader.theVtxBodyParts Is Nothing Then
				Return
			End If
			If theSourceEngineModel.VtxFileHeader.theVtxBodyParts(0).theVtxModels Is Nothing Then
				Return
			End If
			If theSourceEngineModel.VtxFileHeader.theVtxBodyParts(0).theVtxModels(0).theVtxModelLods Is Nothing Then
				Return
			End If

			'referenceSmdFileName = theSourceEngineModel.GetBodyGroupSmdFileName(0, 0, 0)

			'line = ""
			'Me.theOutputFileStream.WriteLine(line)

			''NOTE: Start loop at 1 to skip first LOD, which isn't needed for the $lod command.
			'For lodIndex As Integer = 1 To theSourceEngineModel.theVtxFileHeader.lodCount - 1
			'	Dim switchPoint As Single
			'	'TODO: Need to check that each of these objects exist first before using them.
			'	If lodIndex >= theSourceEngineModel.theVtxFileHeader.theVtxBodyParts(0).theVtxModels(0).theVtxModelLods.Count Then
			'		Return
			'	End If

			'	switchPoint = theSourceEngineModel.theVtxFileHeader.theVtxBodyParts(0).theVtxModels(0).theVtxModelLods(lodIndex).switchPoint

			'	lodSmdFileName = theSourceEngineModel.GetBodyGroupSmdFileName(0, 0, lodIndex)

			'	line = ""
			'	If switchPoint = -1 Then
			'		'// Shadow lod reserves -1 as switch value
			'		'// which uniquely identifies a shadow lod
			'		'newLOD.switchValue = -1.0f;
			'		line += "$shadowlod"
			'	Else
			'		line += "$lod "
			'		line += switchPoint.ToString("0.######", TheApp.InternalNumberFormat)
			'	End If
			'	Me.theOutputFileStream.WriteLine(line)

			'	line = "{"
			'	Me.theOutputFileStream.WriteLine(line)

			'	line = vbTab
			'	line += "replacemodel "
			'	line += """"
			'	line += referenceSmdFileName
			'	line += """ """
			'	line += lodSmdFileName
			'	line += """"
			'	Me.theOutputFileStream.WriteLine(line)

			'	line = "}"
			'	Me.theOutputFileStream.WriteLine(line)
			'Next
			'======
			Dim aBodyPart As SourceVtxBodyPart
			Dim aVtxModel As SourceVtxModel
			Dim aModel As SourceMdlModel
			Dim aLodQcInfo As LodQcInfo
			Dim aLodQcInfoList As List(Of LodQcInfo)
			Dim aLodList As SortedList(Of Single, List(Of LodQcInfo))
			Dim switchPoint As Single

			aLodList = New SortedList(Of Single, List(Of LodQcInfo))()
			For bodyPartIndex As Integer = 0 To Me.theSourceEngineModel.VtxFileHeader.theVtxBodyParts.Count - 1
				aBodyPart = Me.theSourceEngineModel.VtxFileHeader.theVtxBodyParts(bodyPartIndex)

				If aBodyPart.theVtxModels IsNot Nothing Then
					For modelIndex As Integer = 0 To aBodyPart.theVtxModels.Count - 1
						aVtxModel = aBodyPart.theVtxModels(modelIndex)

						If aVtxModel.theVtxModelLods IsNot Nothing Then
							aModel = Me.theSourceEngineModel.MdlFileHeader.theBodyParts(bodyPartIndex).theModels(modelIndex)
							'If aModel.name(0) = ChrW(0) Then
							'	Continue For
							'End If

							'NOTE: Start loop at 1 to skip first LOD, which isn't needed for the $lod command.
							For lodIndex As Integer = 1 To theSourceEngineModel.VtxFileHeader.lodCount - 1
								'TODO: Why would this count be different than the file header count?
								If lodIndex >= aVtxModel.theVtxModelLods.Count Then
									Exit For
								End If

								'If lodIndex = 0 Then
								'    If Not TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked Then
								'        Continue For
								'    End If
								'ElseIf lodIndex > 0 Then
								'    If Not TheApp.Settings.DecompileLodMeshSmdFilesIsChecked Then
								'        Exit For
								'    End If
								'End If

								switchPoint = aVtxModel.theVtxModelLods(lodIndex).switchPoint
								If Not aLodList.ContainsKey(switchPoint) Then
									aLodQcInfoList = New List(Of LodQcInfo)()
									aLodList.Add(switchPoint, aLodQcInfoList)
								Else
									aLodQcInfoList = aLodList(switchPoint)
								End If

								aLodQcInfo = New LodQcInfo()
								aLodQcInfo.referenceFileName = theSourceEngineModel.GetBodyGroupSmdFileName(bodyPartIndex, modelIndex, 0)
								aLodQcInfo.lodFileName = theSourceEngineModel.GetBodyGroupSmdFileName(bodyPartIndex, modelIndex, lodIndex)
								aLodQcInfoList.Add(aLodQcInfo)
							Next
						End If
					Next
				End If
			Next

			line = ""
			Me.theOutputFileStream.WriteLine(line)

			Dim lodQcInfoListOfShadowLod As List(Of LodQcInfo)
			lodQcInfoListOfShadowLod = Nothing

			For lodListIndex As Integer = 0 To aLodList.Count - 1
				switchPoint = aLodList.Keys(lodListIndex)
				If switchPoint = -1 Then
					' Skip writing $shadowlod. Write it last after this loop.
					lodQcInfoListOfShadowLod = aLodList.Values(lodListIndex)
					Continue For
				End If

				aLodQcInfoList = aLodList.Values(lodListIndex)

				line = "$LOD "
				line += switchPoint.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStream.WriteLine(line)

				line = "{"
				Me.theOutputFileStream.WriteLine(line)

				For i As Integer = 0 To aLodQcInfoList.Count - 1
					aLodQcInfo = aLodQcInfoList(i)

					line = vbTab
					line += "replacemodel "
					line += """"
					line += aLodQcInfo.referenceFileName
					line += """ """
					line += aLodQcInfo.lodFileName
					line += """"
					Me.theOutputFileStream.WriteLine(line)
				Next

				line = "}"
				Me.theOutputFileStream.WriteLine(line)
			Next

			'NOTE: As a requirement for the compiler, write $shadowlod last.
			If lodQcInfoListOfShadowLod IsNot Nothing Then
				'// Shadow lod reserves -1 as switch value
				'// which uniquely identifies a shadow lod
				'newLOD.switchValue = -1.0f;
				line = "$ShadowLOD"
				Me.theOutputFileStream.WriteLine(line)

				line = "{"
				Me.theOutputFileStream.WriteLine(line)

				For i As Integer = 0 To lodQcInfoListOfShadowLod.Count - 1
					aLodQcInfo = lodQcInfoListOfShadowLod(i)

					line = vbTab
					line += "replacemodel "
					line += """"
					line += aLodQcInfo.referenceFileName
					line += """ """
					line += aLodQcInfo.lodFileName
					line += """"
					Me.theOutputFileStream.WriteLine(line)
				Next

				line = "}"
				Me.theOutputFileStream.WriteLine(line)
			End If
		End If
	End Sub

	Private Sub WriteNoForcedFadeCommand()
		Dim line As String = ""

		'$noforcedfade
		If (theSourceEngineModel.MdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_NO_FORCED_FADE) > 0 Then
			Me.theOutputFileStream.WriteLine()

			line = "$NoForcedFade"
			Me.theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteForcePhonemeCrossfadeCommand()
		Dim line As String = ""

		'$forcephonemecrossfade
		If (theSourceEngineModel.MdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_FORCE_PHONEME_CROSSFADE) > 0 Then
			Me.theOutputFileStream.WriteLine()

			line = "$ForcePhonemeCrossFade"
			Me.theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WritePoseParameterCommand()
		Dim line As String = ""

		'$poseparameter body_pitch -90.00 90.00 360.00
		'$poseparameter body_yaw -90.00 90.00 360.00
		'$poseparameter head_pitch -90.00 90.00 360.00
		'$poseparameter head_yaw -90.00 90.00 360.00
		If theSourceEngineModel.MdlFileHeader.thePoseParamDescs IsNot Nothing Then
			line = ""
			Me.theOutputFileStream.WriteLine(line)

			For i As Integer = 0 To theSourceEngineModel.MdlFileHeader.thePoseParamDescs.Count - 1
				Dim aPoseParamDesc As SourceMdlPoseParamDesc
				aPoseParamDesc = theSourceEngineModel.MdlFileHeader.thePoseParamDescs(i)
				line = "$PoseParameter """
				line += aPoseParamDesc.theName
				line += """ "
				line += aPoseParamDesc.startingValue.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aPoseParamDesc.endingValue.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aPoseParamDesc.loopingRange.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	Private Sub WriteAmbientBoostCommand()
		Dim line As String = ""

		'$ambientboost
		If (theSourceEngineModel.MdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_AMBIENT_BOOST) > 0 Then
			Me.theOutputFileStream.WriteLine()

			line = "$AmbientBoost"
			Me.theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteOpaqueCommand()
		Dim line As String = ""

		'$mostlyopaque
		'$opaque
		If (theSourceEngineModel.MdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_FORCE_OPAQUE) > 0 Then
			Me.theOutputFileStream.WriteLine()

			line = "$Opaque"
			Me.theOutputFileStream.WriteLine(line)
		ElseIf (theSourceEngineModel.MdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_TRANSLUCENT_TWOPASS) > 0 Then
			Me.theOutputFileStream.WriteLine()

			line = "$MostlyOpaque"
			Me.theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteObsoleteCommand()
		Dim line As String = ""

		'$obsolete
		If (theSourceEngineModel.MdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_OBSOLETE) > 0 Then
			Me.theOutputFileStream.WriteLine()

			line = "$Obsolete"
			Me.theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteCdMaterialsCommand()
		Dim line As String = ""

		'$cdmaterials "models\survivors\producer\"
		'$cdmaterials "models\survivors\"
		'$cdmaterials ""
		If theSourceEngineModel.MdlFileHeader.theTexturePaths IsNot Nothing Then
			line = ""
			Me.theOutputFileStream.WriteLine(line)

			For i As Integer = 0 To theSourceEngineModel.MdlFileHeader.theTexturePaths.Count - 1
				Dim aTexturePath As String
				aTexturePath = theSourceEngineModel.MdlFileHeader.theTexturePaths(i)
				'NOTE: Write out null or empty strings, because Crowbar should show what was stored.
				'If Not String.IsNullOrEmpty(aTexturePath) Then
				line = "$CDMaterials "
				line += """"
				line += aTexturePath
				line += """"
				Me.theOutputFileStream.WriteLine(line)
				'End If
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
		If theSourceEngineModel.MdlFileHeader.theSkinFamilies IsNot Nothing AndAlso theSourceEngineModel.MdlFileHeader.skinReferenceCount > 0 Then
			line = ""
			Me.theOutputFileStream.WriteLine(line)

			line = "$TextureGroup ""skinfamilies"""
			Me.theOutputFileStream.WriteLine(line)
			line = "{"
			Me.theOutputFileStream.WriteLine(line)
			For i As Integer = 0 To theSourceEngineModel.MdlFileHeader.theSkinFamilies.Count - 1
				Dim aSkinFamily As List(Of Integer)
				aSkinFamily = theSourceEngineModel.MdlFileHeader.theSkinFamilies(i)

				line = vbTab
				line += "{"
				Me.theOutputFileStream.WriteLine(line)

				'For j As Integer = 0 To theSourceEngineModel.theMdlFileData.theBodyParts(0).theModels(0).theMeshes.Count - 1
				For j As Integer = 0 To theSourceEngineModel.MdlFileHeader.skinReferenceCount - 1
					'If aSourceEngineModel.theBodyParts(0).theModels(0).theMeshes(j).materialType = 0 Then
					Dim aTexture As SourceMdlTexture
					'aTexture = theSourceEngineModel.theMdlFileHeader.theTextures(j)
					aTexture = theSourceEngineModel.MdlFileHeader.theTextures(aSkinFamily(j))
					line = vbTab
					line += vbTab
					line += """"
					line += aTexture.theName
					line += ".vmt"""
					Me.theOutputFileStream.WriteLine(line)
					'End If
				Next

				line = vbTab
				line += "}"
				Me.theOutputFileStream.WriteLine(line)
			Next
			line = "}"
			Me.theOutputFileStream.WriteLine(line)
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
		If theSourceEngineModel.MdlFileHeader.theTextures IsNot Nothing Then
			Dim line As String

			line = ""
			Me.theOutputFileStream.WriteLine(line)

			line = "// This list shows the VMT files used in the SMD files."
			Me.theOutputFileStream.WriteLine(line)

			For j As Integer = 0 To theSourceEngineModel.MdlFileHeader.theTextures.Count - 1
				Dim aTexture As SourceMdlTexture
				aTexture = theSourceEngineModel.MdlFileHeader.theTextures(j)
				line = "// """
				line += aTexture.theName
				line += ".vmt"""
				Me.theOutputFileStream.WriteLine(line)
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
		If theSourceEngineModel.MdlFileHeader.theAttachments IsNot Nothing Then
			line = ""
			Me.theOutputFileStream.WriteLine(line)

			For i As Integer = 0 To theSourceEngineModel.MdlFileHeader.theAttachments.Count - 1
				Dim anAttachment As SourceMdlAttachment
				anAttachment = theSourceEngineModel.MdlFileHeader.theAttachments(i)
				line = "$Attachment "
				If anAttachment.theName = "" Then
					line += i.ToString(TheApp.InternalNumberFormat)
				Else
					line += """"
					line += anAttachment.theName
					line += """"
				End If
				line += " """
				line += theSourceEngineModel.MdlFileHeader.theBones(anAttachment.localBoneIndex).theName
				line += """"
				line += " "

				If Me.theSourceEngineModel.MdlFileHeader.version = 10 Then
					line += anAttachment.attachmentPoint.x.ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += anAttachment.attachmentPoint.y.ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += anAttachment.attachmentPoint.z.ToString("0.######", TheApp.InternalNumberFormat)
				Else
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
				End If
				Me.theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	Private Sub WriteIncludeModelCommand()
		Dim line As String = ""

		'$includemodel "survivors/anim_producer.mdl"
		'$includemodel "survivors/anim_gestures.mdl"
		If theSourceEngineModel.MdlFileHeader.theModelGroups IsNot Nothing Then
			line = ""
			Me.theOutputFileStream.WriteLine(line)

			For i As Integer = 0 To theSourceEngineModel.MdlFileHeader.theModelGroups.Count - 1
				Dim aModelGroup As SourceMdlModelGroup
				aModelGroup = theSourceEngineModel.MdlFileHeader.theModelGroups(i)
				line = "$IncludeModel "
				line += """"
				If aModelGroup.theFileName.StartsWith("models/") Then
					line += aModelGroup.theFileName.Substring(7)
				Else
					line += aModelGroup.theFileName
				End If
				line += """"
				Me.theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	Private Sub WriteSurfacePropCommand()
		Dim line As String = ""

		If theSourceEngineModel.MdlFileHeader.theSurfacePropName <> "" Then
			line = ""
			Me.theOutputFileStream.WriteLine(line)

			'$surfaceprop "flesh"
			line = "$SurfaceProp "
			line += """"
			line += theSourceEngineModel.MdlFileHeader.theSurfacePropName
			line += """"
			Me.theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteJointSurfacePropCommand()
		Dim line As String = ""

		'$jointsurfaceprop <bone name> <surfaceprop>
		'$jointsurfaceprop "ValveBiped.Bip01_L_Toe0"	 "flesh"
		If theSourceEngineModel.MdlFileHeader.theBones IsNot Nothing Then
			Dim aBone As SourceMdlBone
			Dim emptyLineIsAlreadyWritten As Boolean

			emptyLineIsAlreadyWritten = False
			For i As Integer = 0 To theSourceEngineModel.MdlFileHeader.theBones.Count - 1
				aBone = theSourceEngineModel.MdlFileHeader.theBones(i)

				If aBone.theSurfacePropName <> theSourceEngineModel.MdlFileHeader.theSurfacePropName Then
					If Not emptyLineIsAlreadyWritten Then
						line = ""
						Me.theOutputFileStream.WriteLine(line)
						emptyLineIsAlreadyWritten = True
					End If

					line = "$JointSurfaceProp "
					line += """"
					line += aBone.theName
					line += """"
					line += " "
					line += """"
					line += aBone.theSurfacePropName
					line += """"
					Me.theOutputFileStream.WriteLine(line)
				End If
			Next
		End If
	End Sub

	Private Sub WriteContentsCommand()
		If Me.theSourceEngineModel.MdlFileHeader.contents > 0 Then
			Dim line As String = ""

			line = ""
			Me.theOutputFileStream.WriteLine(line)

			'$contents "monster" "grate"
			line = "$Contents"
			line += Me.GetContentsFlags(Me.theSourceEngineModel.MdlFileHeader.contents)
			Me.theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteJointContentsCommand()
		Dim line As String = ""

		'$jointcontents "<bone_name>" "<content_type_1>" "<content_type_2>" "<content_type_3>"
		If theSourceEngineModel.MdlFileHeader.theBones IsNot Nothing Then
			Dim aBone As SourceMdlBone
			Dim emptyLineIsAlreadyWritten As Boolean

			emptyLineIsAlreadyWritten = False
			For i As Integer = 0 To theSourceEngineModel.MdlFileHeader.theBones.Count - 1
				aBone = theSourceEngineModel.MdlFileHeader.theBones(i)

				If aBone.contents <> Me.theSourceEngineModel.MdlFileHeader.contents Then
					If Not emptyLineIsAlreadyWritten Then
						line = ""
						Me.theOutputFileStream.WriteLine(line)
						emptyLineIsAlreadyWritten = True
					End If

					line = "$JointContents "
					line += """"
					line += aBone.theName
					line += """"
					line += Me.GetContentsFlags(aBone.contents)
					Me.theOutputFileStream.WriteLine(line)
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

		offsetX = Math.Round(theSourceEngineModel.MdlFileHeader.eyePositionY, 3)
		offsetY = -Math.Round(theSourceEngineModel.MdlFileHeader.eyePositionX, 3)
		offsetZ = Math.Round(theSourceEngineModel.MdlFileHeader.eyePositionZ, 3)

		If offsetX = 0 AndAlso offsetY = 0 AndAlso offsetZ = 0 Then
			Exit Sub
		End If

		line = ""
		Me.theOutputFileStream.WriteLine(line)

		'$eyeposition -0.000 0.000 70.000
		'NOTE: These are stored in different order in MDL file.
		'FROM: utils\studiomdl\studiomdl.cpp Cmd_Eyeposition()
		'eyeposition[1] = verify_atof (token);
		'eyeposition[0] = -verify_atof (token);
		'eyeposition[2] = verify_atof (token);
		line = "$EyePosition "
		line += offsetX.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += offsetY.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += offsetZ.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteMaxEyeDeflectionCommand()
		Dim line As String = ""
		Dim deflection As Double

		'FROM: SourceEngine2007_source\src_main\utils\studiomdl\studiomdl.cpp
		'	g_flMaxEyeDeflection = cosf( verify_atof( token ) * M_PI / 180.0f );
		deflection = Math.Acos(theSourceEngineModel.MdlFileHeader.maxEyeDeflection)
		deflection = MathModule.RadiansToDegrees(deflection)
		deflection = Math.Round(deflection, 3)

		line = ""
		Me.theOutputFileStream.WriteLine(line)

		' Found in L4D2 file: "survivors\Biker\biker.qc".
		'// Eyes can look this many degrees up/down/to the sides
		'$maxeyedeflection 30
		line = "$MaxEyeDeflection "
		line += deflection.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteIllumPositionCommand()
		Dim line As String = ""
		Dim offsetX As Double
		Dim offsetY As Double
		Dim offsetZ As Double

		line = ""
		Me.theOutputFileStream.WriteLine(line)

		line = ""
		line += "// "
		line += "Only set this if you know what it does, and need it for special circumstances, such as with gibs."
		Me.theOutputFileStream.WriteLine(line)

		'$illumposition -2.533 -0.555 32.487
		'NOTE: These are stored in different order in MDL file.
		'FROM: utils\studiomdl\studiomdl.cpp Cmd_Illumposition()
		'illumposition[1] = verify_atof (token);
		'illumposition[0] = -verify_atof (token);
		'illumposition[2] = verify_atof (token);
		offsetX = Math.Round(theSourceEngineModel.MdlFileHeader.illuminationPositionY, 3)
		offsetY = -Math.Round(theSourceEngineModel.MdlFileHeader.illuminationPositionX, 3)
		offsetZ = Math.Round(theSourceEngineModel.MdlFileHeader.illuminationPositionZ, 3)
		line = ""
		line += "// "
		line += "$IllumPosition "
		line += offsetX.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += offsetY.ToString("0.######", TheApp.InternalNumberFormat)
		line += " "
		line += offsetZ.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteGroupAnimation()
		Me.WriteAnimBlockSizeCommand()
		Me.WriteSectionFramesCommand()
		Me.WritePoseParameterCommand()
		Me.FillInWeightLists()
		'NOTE: Must write $WeightList lines before animations or sequences that use them.
		Me.WriteWeightListCommand()
		'NOTE: Must write $animation lines before $sequence lines that use them.
		Try
			Me.WriteAnimationOrDeclareAnimationCommand()
		Catch ex As Exception
		End Try
		Try
			Me.WriteSequenceOrDeclareSequenceCommand()
		Catch ex As Exception
		End Try
		Me.WriteIncludeModelCommand()
		Me.WriteIkChainCommand()
		Me.WriteIkAutoPlayLockCommand()
		Me.WriteBoneSaveFrameCommand()
	End Sub

	Private Sub WriteAnimBlockSizeCommand()
		Dim line As String = ""

		'$animblocksize 32 nostall highres
		If theSourceEngineModel.MdlFileHeader.animBlockCount > 0 Then
			Me.theOutputFileStream.WriteLine()

			line = "// The 32 below is a guess until further is known about the format."
			Me.theOutputFileStream.WriteLine(line)

			line = "$AnimBlockSize"
			line += " "
			line += "32"
			'line += " "
			'line += "nostall"
			'line += " "
			'line += "highres"
			Me.theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteSectionFramesCommand()
		Dim line As String = ""

		'$sectionframes
		If theSourceEngineModel.MdlFileHeader.theSectionFrameCount > 0 Then
			Me.theOutputFileStream.WriteLine()

			line = "$SectionFrames"
			line += " "
			line += theSourceEngineModel.MdlFileHeader.theSectionFrameCount.ToString(TheApp.InternalNumberFormat)
			line += " "
			line += theSourceEngineModel.MdlFileHeader.theSectionFrameMinFrameCount.ToString(TheApp.InternalNumberFormat)
			Me.theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub FillInWeightLists()
		If Me.theSourceEngineModel.MdlFileHeader.theSequenceDescs IsNot Nothing Then
			Dim aSeqDesc As SourceMdlSequenceDesc
			Dim aWeightList As SourceMdlWeightList
			Dim aWeightListIndex As Integer

			For i As Integer = 0 To Me.theSourceEngineModel.MdlFileHeader.theSequenceDescs.Count - 1
				aSeqDesc = Me.theSourceEngineModel.MdlFileHeader.theSequenceDescs(i)

				Try
					If aSeqDesc.theBoneWeights IsNot Nothing AndAlso aSeqDesc.theBoneWeights.Count > 0 AndAlso Not aSeqDesc.theBoneWeightsAreDefault Then
						For aWeightListIndex = 0 To Me.theSourceEngineModel.MdlFileHeader.theWeightLists.Count - 1
							aWeightList = Me.theSourceEngineModel.MdlFileHeader.theWeightLists(aWeightListIndex)

							If GenericsModule.ListsAreEqual(aSeqDesc.theBoneWeights, aWeightList.theWeights) Then
								Exit For
							End If
						Next

						If aWeightListIndex < Me.theSourceEngineModel.MdlFileHeader.theWeightLists.Count Then
							aSeqDesc.theWeightListIndex = aWeightListIndex
						Else
							aWeightList = New SourceMdlWeightList()

							'NOTE: Name is not stored, so use something reasonable.
							aWeightList.theName = "weights_" + aSeqDesc.theName
							For Each value As Double In aSeqDesc.theBoneWeights
								aWeightList.theWeights.Add(value)
							Next

							Me.theSourceEngineModel.MdlFileHeader.theWeightLists.Add(aWeightList)

							aSeqDesc.theWeightListIndex = Me.theSourceEngineModel.MdlFileHeader.theWeightLists.Count - 1
						End If
					End If
				Catch ex As Exception
					Dim debug As Integer = 4242
				End Try
			Next
		End If
	End Sub

	Private Sub WriteWeightListCommand()
		Dim line As String = ""
		Dim commentTag As String = ""

		''NOTE: Comment-out for now, because some models will not recompile with them.
		'commentTag = "// "

		'$weightlist top_bottom {
		'	"Bone_1" 0
		'	"Bone_2" 0.25
		'	"Bone_3" 0.5
		'	"Bone_4" 0.75
		'	"Bone_5" 1
		'}
		'If Me.theSourceEngineModel.MdlFileHeader.theSequenceDescs IsNot Nothing Then
		'	Me.theOutputFileStream.WriteLine()

		'	For i As Integer = 0 To Me.theSourceEngineModel.MdlFileHeader.theSequenceDescs.Count - 1
		'		Dim aSeqDesc As SourceMdlSequenceDesc
		'		aSeqDesc = Me.theSourceEngineModel.MdlFileHeader.theSequenceDescs(i)

		'		If aSeqDesc.theBoneWeights IsNot Nothing AndAlso aSeqDesc.theBoneWeights.Count > 0 AndAlso Not aSeqDesc.theBoneWeightsAreDefault Then
		'			line = "$WeightList "
		'			'NOTE: Name is not stored, so use something reasonable.
		'			line += """"
		'			line += "weights_"
		'			line += aSeqDesc.theName
		'			line += """"
		'			'NOTE: The opening brace must be on same line as the command.
		'			line += " {"
		'			Me.theOutputFileStream.WriteLine(commentTag + line)

		'			For boneWeightIndex As Integer = 0 To aSeqDesc.theBoneWeights.Count - 1
		'				line = vbTab
		'				line += " """
		'				line += Me.theSourceEngineModel.MdlFileHeader.theBones(boneWeightIndex).theName
		'				line += """ "
		'				line += aSeqDesc.theBoneWeights(boneWeightIndex).ToString("0.######", TheApp.InternalNumberFormat)
		'				Me.theOutputFileStream.WriteLine(commentTag + line)
		'			Next

		'			line = "}"
		'			Me.theOutputFileStream.WriteLine(commentTag + line)
		'		End If
		'	Next
		'End If
		For Each aWeightList As SourceMdlWeightList In Me.theSourceEngineModel.MdlFileHeader.theWeightLists
			Me.theOutputFileStream.WriteLine()

			line = "$WeightList "
			line += """"
			line += aWeightList.theName
			line += """"
			'NOTE: The opening brace must be on same line as the command.
			line += " {"
			Me.theOutputFileStream.WriteLine(commentTag + line)

			For boneWeightIndex As Integer = 0 To aWeightList.theWeights.Count - 1
				line = vbTab
				line += " """
				line += Me.theSourceEngineModel.MdlFileHeader.theBones(boneWeightIndex).theName
				line += """ "
				line += aWeightList.theWeights(boneWeightIndex).ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStream.WriteLine(commentTag + line)
			Next

			line = "}"
			Me.theOutputFileStream.WriteLine(commentTag + line)
		Next
	End Sub

	Private Sub WriteAnimationOrDeclareAnimationCommand()
		If Me.theSourceEngineModel.MdlFileHeader.theAnimationDescs IsNot Nothing Then
			For i As Integer = 0 To Me.theSourceEngineModel.MdlFileHeader.theAnimationDescs.Count - 1
				Dim anAnimationDesc As SourceMdlAnimationDesc
				anAnimationDesc = Me.theSourceEngineModel.MdlFileHeader.theAnimationDescs(i)

				If anAnimationDesc.theName(0) <> "@" Then
					Me.WriteAnimationLine(anAnimationDesc)
				End If
			Next
		End If
	End Sub

	Private Sub WriteAnimationLine(ByVal anAnimationDesc As SourceMdlAnimationDesc)
		Dim line As String = ""

		Me.theOutputFileStream.WriteLine()

		If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_OVERRIDE) > 0 Then
			line = "$DeclareAnimation"
			line += " """
			'TODO: Does this need to check and remove initial "@" from name?
			line += anAnimationDesc.theName
			line += """"
			Me.theOutputFileStream.WriteLine(line)
		Else
			'$animation a_reference "primary_idle.dmx" lx ly
			'NOTE: The $Animation command must have name first and file name second and on same line as the command.
			line = "$Animation"
			line += " """
			line += anAnimationDesc.theName
			line += """ """
			line += Me.theSourceEngineModel.GetAnimationSmdRelativePathFileName(anAnimationDesc)
			line += """"
			'NOTE: Opening brace must be on same line as the command.
			line += " {"
			Me.theOutputFileStream.WriteLine(line)

			Me.WriteAnimationOptions(Nothing, anAnimationDesc, Nothing)

			line = "}"
			Me.theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteSequenceOrDeclareSequenceCommand()
		'$sequence producer "producer" fps 30.00
		'$sequence ragdoll "ragdoll" ACT_DIERAGDOLL 1 fps 30.00
		If Me.theSourceEngineModel.MdlFileHeader.theSequenceDescs IsNot Nothing Then
			For i As Integer = 0 To Me.theSourceEngineModel.MdlFileHeader.theSequenceDescs.Count - 1
				Dim aSequenceDesc As SourceMdlSequenceDesc
				aSequenceDesc = Me.theSourceEngineModel.MdlFileHeader.theSequenceDescs(i)

				Me.WriteSequenceLine(aSequenceDesc)
			Next
		End If
	End Sub

	Private Sub WriteSequenceLine(ByVal aSequenceDesc As SourceMdlSequenceDesc)
		Dim line As String = ""

		Me.theOutputFileStream.WriteLine()

		If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_OVERRIDE) > 0 Then
			line = "$DeclareSequence"
			line += " """
			line += aSequenceDesc.theName
			line += """"
			Me.theOutputFileStream.WriteLine(line)
		Else
			If aSequenceDesc.theAnimDescIndexes IsNot Nothing OrElse aSequenceDesc.theAnimDescIndexes.Count > 0 Then
				line = "$Sequence"
				line += " """
				line += aSequenceDesc.theName
				line += """"
				'NOTE: Opening brace must be on same line as the command.
				line += " {"
				Me.theOutputFileStream.WriteLine(line)

				Me.WriteSequenceOptions(aSequenceDesc)

				line = "}"
				Me.theOutputFileStream.WriteLine(line)
			End If
		End If
	End Sub

	'activity           // done
	'activitymodifier   // v49; done
	'addlayer           // done
	'autoplay           // done
	'blend              // done
	'blendcenter        // baked-in
	'blendcomp          // baked-in
	'blendlayer         // done
	'blendwidth         // done
	'blendref           // baked-in
	'calcblend          // baked-in
	'delta              // done
	'event              // done
	'exitphase          // not used
	'fadein             // done
	'fadeout            // done
	'hidden             // done
	'iklock             // done
	'keyvalues          // done
	'node               // done
	'posecycle          // v48; done
	'post               // baked-in
	'predelta           // done
	'realtime           // done
	'rtransition        // done
	'snap               // done
	'transition         // done
	'worldspace         // done       
	'ParseAnimationToken( animations[0] )
	'Cmd_ImpliedAnimation( pseq, token )
	Private Sub WriteSequenceOptions(ByVal aSequenceDesc As SourceMdlSequenceDesc)
		Dim line As String = ""
		Dim valueString As String
		Dim impliedAnimDesc As SourceMdlAnimationDesc = Nothing

		Dim anAnimationDesc As SourceMdlAnimationDesc
		Dim name As String
		For j As Integer = 0 To aSequenceDesc.theAnimDescIndexes.Count - 1
			anAnimationDesc = Me.theSourceEngineModel.MdlFileHeader.theAnimationDescs(aSequenceDesc.theAnimDescIndexes(j))
			name = anAnimationDesc.theName

			line = vbTab
			line += """"
			If name(0) = "@" Then
				'NOTE: There should only be one implied anim desc.
				impliedAnimDesc = anAnimationDesc
				line += Me.theSourceEngineModel.GetAnimationSmdRelativePathFileName(anAnimationDesc)
			Else
				line += name
			End If
			line += """"
			Me.theOutputFileStream.WriteLine(line)
		Next

		If aSequenceDesc.theActivityName <> "" Then
			line = vbTab
			line += "activity "
			line += """"
			line += aSequenceDesc.theActivityName
			line += """ "
			line += aSequenceDesc.activityWeight.ToString(TheApp.InternalNumberFormat)
			Me.theOutputFileStream.WriteLine(line)
		End If

		If aSequenceDesc.theActivityModifiers IsNot Nothing Then
			For Each activityModifier As SourceMdlActivityModifier In aSequenceDesc.theActivityModifiers
				line = vbTab
				line += "activitymodifier "
				line += activityModifier.theName
				Me.theOutputFileStream.WriteLine(line)
			Next
		End If

		If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_AUTOPLAY) > 0 Then
			line = vbTab
			line += "autoplay"
			Me.theOutputFileStream.WriteLine(line)
		End If

		Me.WriteSequenceBlendInfo(aSequenceDesc)

		If aSequenceDesc.groupSize(0) <> aSequenceDesc.groupSize(1) Then
			line = vbTab
			line += "blendwidth "
			line += aSequenceDesc.groupSize(0).ToString(TheApp.InternalNumberFormat)
			Me.theOutputFileStream.WriteLine(line)
		End If

		Me.WriteSequenceDeltaInfo(aSequenceDesc)

		If aSequenceDesc.theEvents IsNot Nothing Then
			Dim frameIndex As Integer
			Dim frameCount As Integer
			frameCount = Me.theSourceEngineModel.MdlFileHeader.theAnimationDescs(aSequenceDesc.theAnimDescIndexes(0)).frameCount
			For j As Integer = 0 To aSequenceDesc.theEvents.Count - 1
				If frameCount <= 1 Then
					frameIndex = 0
				Else
					frameIndex = CInt(aSequenceDesc.theEvents(j).cycle * (frameCount - 1))
				End If
				line = vbTab
				line += "{ "
				line += "event "
				line += aSequenceDesc.theEvents(j).theName
				line += " "
				line += frameIndex.ToString(TheApp.InternalNumberFormat)
				If aSequenceDesc.theEvents(j).options <> "" Then
					line += " """
					line += CStr(aSequenceDesc.theEvents(j).options).Trim(Chr(0))
					line += """"
				End If
				line += " }"
				Me.theOutputFileStream.WriteLine(line)
			Next
		End If

		valueString = aSequenceDesc.fadeInTime.ToString("0.######", TheApp.InternalNumberFormat)
		line = vbTab
		line += "fadein "
		line += valueString
		Me.theOutputFileStream.WriteLine(line)

		valueString = aSequenceDesc.fadeOutTime.ToString("0.######", TheApp.InternalNumberFormat)
		line = vbTab
		line += "fadeout "
		line += valueString
		Me.theOutputFileStream.WriteLine(line)

		If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_HIDDEN) > 0 Then
			line = vbTab
			line += "hidden"
			Me.theOutputFileStream.WriteLine(line)
		End If

		'If aSeqDesc.theIkLocks IsNot Nothing AndAlso Me.theSourceEngineModel.theMdlFileHeader.theIkLocks IsNot Nothing AndAlso Me.theSourceEngineModel.theMdlFileHeader.theIkChains IsNot Nothing Then
		If aSequenceDesc.theIkLocks IsNot Nothing AndAlso Me.theSourceEngineModel.MdlFileHeader.theIkChains IsNot Nothing Then
			Dim ikLock As SourceMdlIkLock

			For ikLockIndex As Integer = 0 To aSequenceDesc.theIkLocks.Count - 1
				'If ikLockIndex >= Me.theSourceEngineModel.theMdlFileHeader.theIkLocks.Count Then
				'	Continue For
				'End If
				'ikLock = Me.theSourceEngineModel.theMdlFileHeader.theIkLocks(ikLockIndex)
				ikLock = aSequenceDesc.theIkLocks(ikLockIndex)

				'iklock <chain name> <pos lock> <angle lock>
				line = vbTab
				line += "iklock """
				line += Me.theSourceEngineModel.MdlFileHeader.theIkChains(ikLock.chainIndex).theName
				line += """"
				line += " "
				line += ikLock.posWeight.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += ikLock.localQWeight.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStream.WriteLine(line)
			Next
		End If

		Me.WriteKeyValues(aSequenceDesc.theKeyValues, "keyvalues")

		Me.WriteSequenceLayerInfo(aSequenceDesc)

		Me.WriteSequenceNodeInfo(aSequenceDesc)

		If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_CYCLEPOSE) > 0 Then
			line = vbTab
			line += "posecycle "
			line += aSequenceDesc.cyclePoseIndex.ToString(TheApp.InternalNumberFormat)
			Me.theOutputFileStream.WriteLine(line)
		End If

		If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_REALTIME) > 0 Then
			line = vbTab
			line += "realtime"
			Me.theOutputFileStream.WriteLine(line)
		End If

		If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_SNAP) > 0 Then
			line = vbTab
			line += "snap"
			Me.theOutputFileStream.WriteLine(line)
		End If

		If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_WORLD) > 0 Then
			line = vbTab
			line += "worldspace"
			Me.theOutputFileStream.WriteLine(line)
		End If

		'If blah Then
		'	line = vbTab
		'	line += ""
		'	Me.theOutputFileStream.WriteLine(line)
		'End If

		Dim firstAnimDesc As SourceMdlAnimationDesc
		firstAnimDesc = Me.theSourceEngineModel.MdlFileHeader.theAnimationDescs(aSequenceDesc.theAnimDescIndexes(0))
		Me.WriteAnimationOptions(aSequenceDesc, firstAnimDesc, impliedAnimDesc)
	End Sub

	'angles
	'autoik
	'blockname
	'cmdlist
	'fps                // done
	'frame              // baked-in and decompiles as separate anim smd files
	'fudgeloop
	'if
	'loop               // done
	'motionrollback
	'noanimblock
	'noanimblockstall
	'noautoik
	'origin
	'post               // baked-in 
	'rotate
	'scale
	'snap               // ($sequence version handled elsewhere)
	'startloop
	'ParseCmdlistToken( panim->numcmds, panim->cmds )
	'TODO: All these options (LX, LY, etc.) seem to be baked-in, but might need to be calculated for anims that have movement.
	'lookupControl( token )       
	Private Sub WriteAnimationOptions(ByVal aSequenceDesc As SourceMdlSequenceDesc, ByVal anAnimationDesc As SourceMdlAnimationDesc, ByVal impliedAnimDesc As SourceMdlAnimationDesc)
		Dim line As String = ""

		line = vbTab
		line += "fps "
		line += anAnimationDesc.fps.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStream.WriteLine(line)

		If aSequenceDesc Is Nothing Then
			If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_LOOPING) > 0 Then
				line = vbTab
				line += "loop"
				Me.theOutputFileStream.WriteLine(line)
			End If
		Else
			If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_LOOPING) > 0 Then
				line = vbTab
				line += "loop"
				Me.theOutputFileStream.WriteLine(line)
			End If
		End If

		Me.WriteCmdListOptions(aSequenceDesc, anAnimationDesc, impliedAnimDesc)
	End Sub

	'align
	'alignbone
	'alignboneto
	'alignto
	'compress
	'counterrotate
	'counterrotateto
	'derivative
	'fixuploop          // baked-in
	'ikfixup
	'ikrule
	'lineardelta
	'localhierarchy     // done
	'match
	'matchblend
	'noanimation        // done
	'numframes
	'presubtract
	'rotateto
	'splinedelta
	'subtract
	'walkalign
	'walkalignto
	'walkframe
	'weightlist         // done
	'worldspaceblend       //
	'worldspaceblendloop   // 
	Private Sub WriteCmdListOptions(ByVal aSequenceDesc As SourceMdlSequenceDesc, ByVal anAnimationDesc As SourceMdlAnimationDesc, ByVal impliedAnimDesc As SourceMdlAnimationDesc)
		Dim line As String = ""

		'$sequence taunt01 "taunt01.dmx" fps 30 localhierarchy "weapon_bone" "bip_hand_L" range 0 5 80 90 {
		'if (srcanim->numframes > 1.0)
		'{
		'	pHierarchy->start	= srcanim->localhierarchy[j].start / (srcanim->numframes - 1.0f);
		'	pHierarchy->peak	= srcanim->localhierarchy[j].peak / (srcanim->numframes - 1.0f);
		'	pHierarchy->tail	= srcanim->localhierarchy[j].tail / (srcanim->numframes - 1.0f);
		'	pHierarchy->end		= srcanim->localhierarchy[j].end / (srcanim->numframes - 1.0f);
		'}
		'              Else
		'{
		'	pHierarchy->start	= 0.0f;
		'	pHierarchy->peak	= 0.0f;
		'	pHierarchy->tail	= 1.0f;
		'	pHierarchy->end		= 1.0f;
		'}
		'NOTE: Reverse calculation of above: qc = mdl * (srcanim->numframes - 1.0f)
		If impliedAnimDesc Is Nothing Then
			Me.WriteCmdListLocalHierarchyOption(anAnimationDesc)
		Else
			Me.WriteCmdListLocalHierarchyOption(impliedAnimDesc)
		End If

		If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) > 0 Then
			line = vbTab
			line += "noanimation"
			Me.theOutputFileStream.WriteLine(line)
		End If

		'TODO: This seems valid according to source code, but it checks same flag (STUDIO_DELTA) as "delta" option.
		'      Unsure how to determine which option is intended or if both are intended.
		If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_DELTA) > 0 Then
			line = vbTab
			line += "// This subtract line guesses the animation name and frame index. There is no way to determine which $animation and which frame was used. Change as needed."
			Me.theOutputFileStream.WriteLine(line)

			line = vbTab
			'line += "// "
			line += "subtract"
			line += " """
			'TODO: Change to writing anim_name.
			' Doesn't seem to be direct way to get this name.
			' For now, do what MDL Decompiler seems to do; use the first animation name.
			'line += "[anim_name]"
			'line += Me.theFirstAnimationDescName
			line += Me.theSourceEngineModel.MdlFileHeader.theFirstAnimationDesc.theName
			line += """ "
			'TODO: Change to writing frameIndex.
			' Doesn't seem to be direct way to get this value.
			' For now, do what MDL Decompiler seems to do; use zero for the frameIndex.
			'line += "[frameIndex]"
			line += "0"
			Me.theOutputFileStream.WriteLine(line)
		End If

		'TODO: Can probably reduce the info written in v0.24.
		' weightlist "top_bottom"
		Dim aSeqDesc As SourceMdlSequenceDesc = Nothing
		If aSequenceDesc Is Nothing Then
			If anAnimationDesc.theAnimIsLinkedToSequence Then
				'NOTE: Just get first one, because all should have same bone weights.
				aSeqDesc = anAnimationDesc.theLinkedSequences(0)
			End If
		Else
			aSeqDesc = aSequenceDesc
		End If
		'If aSeqDesc IsNot Nothing AndAlso aSeqDesc.theBoneWeights IsNot Nothing AndAlso aSeqDesc.theBoneWeights.Count > 0 AndAlso Not aSeqDesc.theBoneWeightsAreDefault Then
		'	Me.WriteSequenceWeightListLine(aSeqDesc)
		'End If
		If aSeqDesc IsNot Nothing AndAlso aSeqDesc.theWeightListIndex > -1 Then
			Me.WriteSequenceWeightListLine(aSeqDesc)
		End If
	End Sub

	Private Sub WriteSequenceWeightListLine(ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim line As String = ""

		line = vbTab
		line += "weightlist "
		'NOTE: Name is not stored, so use something reasonable. Needs to be the same as used in $weightlist.
		line += """"
		'line += "weights_"
		'line += aSeqDesc.theName
		line += Me.theSourceEngineModel.MdlFileHeader.theWeightLists(aSeqDesc.theWeightListIndex).theName
		line += """"
		Me.theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteSequenceBlendInfo(ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim line As String = ""

		For i As Integer = 0 To 1
			If aSeqDesc.paramIndex(i) <> -1 Then
				line = vbTab
				line += "blend "
				line += """"
				line += theSourceEngineModel.MdlFileHeader.thePoseParamDescs(aSeqDesc.paramIndex(i)).theName
				line += """"
				line += " "
				line += aSeqDesc.paramStart(i).ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aSeqDesc.paramEnd(i).ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStream.WriteLine(line)
			End If
		Next
	End Sub

	Private Sub WriteSequenceDeltaInfo(ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim line As String = ""

		If (aSeqDesc.flags And SourceMdlAnimationDesc.STUDIO_DELTA) > 0 Then
			If (aSeqDesc.flags And SourceMdlAnimationDesc.STUDIO_POST) > 0 Then
				line = vbTab
				'line += "// "
				line += "delta"
				Me.theOutputFileStream.WriteLine(line)
			Else
				line = vbTab
				line += "predelta"
				Me.theOutputFileStream.WriteLine(line)
			End If
		End If
	End Sub

	Private Sub WriteSequenceLayerInfo(ByVal aSeqDesc As SourceMdlSequenceDesc)
		If aSeqDesc.autoLayerCount > 0 Then
			Dim line As String = ""
			Dim layer As SourceMdlAutoLayer
			Dim otherSequenceName As String

			For j As Integer = 0 To aSeqDesc.theAutoLayers.Count - 1
				layer = aSeqDesc.theAutoLayers(j)
				otherSequenceName = theSourceEngineModel.MdlFileHeader.theSequenceDescs(layer.sequenceIndex).theName

				If layer.flags = 0 Then
					'addlayer <string|other $sequence name>
					line = vbTab
					'line += "// "
					line += "addlayer "
					line += """"
					line += otherSequenceName
					line += """"
					Me.theOutputFileStream.WriteLine(line)
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
						If Me.theSourceEngineModel.MdlFileHeader.thePoseParamDescs IsNot Nothing AndAlso Me.theSourceEngineModel.MdlFileHeader.thePoseParamDescs.Count > layer.poseIndex Then
							line += " poseparameter"
							line += " "
							line += Me.theSourceEngineModel.MdlFileHeader.thePoseParamDescs(layer.poseIndex).theName
						End If
					End If
					If (layer.flags And SourceMdlAutoLayer.STUDIO_AL_LOCAL) > 0 Then
						line += " local"
					End If

					Me.theOutputFileStream.WriteLine(line)
				End If
			Next
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
				line += theSourceEngineModel.MdlFileHeader.theLocalNodeNames(aSeqDesc.localEntryNodeIndex - 1)
				line += """"
				Me.theOutputFileStream.WriteLine(line)
			ElseIf (aSeqDesc.nodeFlags And 1) = 0 Then
				'transition (from) (to) 
				line = vbTab
				line += "transition"
				line += " """
				'NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
				line += theSourceEngineModel.MdlFileHeader.theLocalNodeNames(aSeqDesc.localEntryNodeIndex - 1)
				line += """ """
				'NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
				line += theSourceEngineModel.MdlFileHeader.theLocalNodeNames(aSeqDesc.localExitNodeIndex - 1)
				line += """"
				Me.theOutputFileStream.WriteLine(line)
			Else
				'rtransition (name1) (name2) 
				line = vbTab
				line += "rtransition"
				line += " """
				'NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
				line += theSourceEngineModel.MdlFileHeader.theLocalNodeNames(aSeqDesc.localEntryNodeIndex - 1)
				line += """ """
				'NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
				line += theSourceEngineModel.MdlFileHeader.theLocalNodeNames(aSeqDesc.localExitNodeIndex - 1)
				line += """"
				Me.theOutputFileStream.WriteLine(line)
			End If
		End If
	End Sub

	Private Sub WriteCmdListLocalHierarchyOption(ByVal anAnimationDesc As SourceMdlAnimationDesc)
		Dim line As String = ""

		If anAnimationDesc.theLocalHierarchies IsNot Nothing AndAlso anAnimationDesc.theLocalHierarchies.Count > 0 Then
			Dim aLocalHierarchy As SourceMdlLocalHierarchy
			Dim frameCount As Integer
			Dim startInfluence As Double
			Dim peakInfluence As Double
			Dim tailInfluence As Double
			Dim endInfluence As Double

			aLocalHierarchy = anAnimationDesc.theLocalHierarchies(0)
			frameCount = anAnimationDesc.frameCount
			startInfluence = aLocalHierarchy.startInfluence * (frameCount - 1)
			peakInfluence = aLocalHierarchy.peakInfluence * (frameCount - 1)
			tailInfluence = aLocalHierarchy.tailInfluence * (frameCount - 1)
			endInfluence = aLocalHierarchy.endInfluence * (frameCount - 1)

			line = vbTab
			line += "localhierarchy"
			line += " """
			line += Me.theSourceEngineModel.MdlFileHeader.theBones(aLocalHierarchy.boneIndex).theName
			line += """"
			line += " """
			line += Me.theSourceEngineModel.MdlFileHeader.theBones(aLocalHierarchy.boneNewParentIndex).theName
			line += """"
			line += " range "
			line += startInfluence.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += peakInfluence.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += tailInfluence.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += endInfluence.ToString("0.######", TheApp.InternalNumberFormat)
			'line += aLocalHierarchy.boneIndex.ToString("0.######", TheApp.InternalNumberFormat)
			Me.theOutputFileStream.WriteLine(line)
		End If
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
			If theSourceEngineModel.MdlFileHeader.theIkChains IsNot Nothing Then
				line = ""
				Me.theOutputFileStream.WriteLine(line)

				For i As Integer = 0 To theSourceEngineModel.MdlFileHeader.theIkChains.Count - 1
					Dim boneIndex As Integer = theSourceEngineModel.MdlFileHeader.theIkChains(i).theLinks(theSourceEngineModel.MdlFileHeader.theIkChains(i).theLinks.Count - 1).boneIndex
					offsetX = Math.Round(theSourceEngineModel.MdlFileHeader.theIkChains(i).theLinks(0).idealBendingDirection.x, 3)
					offsetY = Math.Round(theSourceEngineModel.MdlFileHeader.theIkChains(i).theLinks(0).idealBendingDirection.y, 3)
					offsetZ = Math.Round(theSourceEngineModel.MdlFileHeader.theIkChains(i).theLinks(0).idealBendingDirection.z, 3)

					line = "$IKChain """
					line += theSourceEngineModel.MdlFileHeader.theIkChains(i).theName
					line += """ """
					line += theSourceEngineModel.MdlFileHeader.theBones(boneIndex).theName
					line += """ knee "
					line += offsetX.ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += offsetY.ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += offsetZ.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)
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
			If Me.theSourceEngineModel.MdlFileHeader.theIkLocks IsNot Nothing Then
				line = ""
				Me.theOutputFileStream.WriteLine(line)

				For i As Integer = 0 To Me.theSourceEngineModel.MdlFileHeader.theIkLocks.Count - 1
					ikLock = Me.theSourceEngineModel.MdlFileHeader.theIkLocks(i)

					line = "$IKAutoPlayLock """
					line += Me.theSourceEngineModel.MdlFileHeader.theIkChains(ikLock.chainIndex).theName
					line += """"
					line += " "
					line += ikLock.posWeight.ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += ikLock.localQWeight.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)
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
			If Me.theSourceEngineModel.MdlFileHeader.theBones IsNot Nothing Then
				Dim aBone As SourceMdlBone
				Dim emptyLineIsAlreadyWritten As Boolean

				emptyLineIsAlreadyWritten = False
				For i As Integer = 0 To theSourceEngineModel.MdlFileHeader.theBones.Count - 1
					aBone = theSourceEngineModel.MdlFileHeader.theBones(i)

					If (aBone.flags And SourceMdlBone.BONE_HAS_SAVEFRAME_POS) > 0 OrElse (aBone.flags And SourceMdlBone.BONE_HAS_SAVEFRAME_ROT) > 0 Then
						If Not emptyLineIsAlreadyWritten Then
							line = ""
							Me.theOutputFileStream.WriteLine(line)
							emptyLineIsAlreadyWritten = True
						End If

						line = "$BoneSaveFrame "
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
						Me.theOutputFileStream.WriteLine(line)
					End If
				Next
			End If
		Catch ex As Exception

		End Try
	End Sub

	Private Sub WriteGroupCollision()
		Me.WriteCollisionModelOrCollisionJointsCommand()
		Me.WriteCollisionTextCommand()
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
		If theSourceEngineModel.PhyFileHeader IsNot Nothing AndAlso Me.theSourceEngineModel.PhyFileHeader.solidCount > 0 Then
			Me.theOutputFileStream.WriteLine(line)

			'If Me.theSourceEngineModel.PhyFileHeader.checksum <> Me.theSourceEngineModel.MdlFileHeader.checksum Then
			'	line = "// The PHY file's checksum value is not the same as the MDL file's checksum value."
			'	Me.theOutputFileStream.WriteLine(line)
			'End If

			'NOTE: The smd file name for $collisionjoints is not stored in the mdl file, 
			'      so use the same name that MDL Decompiler uses.
			'TODO: Find a better way to determine which to use.
			'NOTE: "If Me.theSourceEngineModel.theMdlFileHeader.theAnimationDescs.Count < 2" 
			'      works for survivors but not for witch (which has only one sequence).
			'If theSourceEngineModel.thePhyFileHeader.theSourcePhyPhysCollisionModels.Count < 2 Then
			'TODO: Why not use this "if" statement? It seems reasonable that a solid matches a set of convex shapes for one bone.
			'      For example, L4D2 van has several convex shapes, but only one solid and one bone.
			'      Same for w_minigun. Both use $concave.
			'If Me.theSourceEngineModel.thePhyFileHeader.solidCount = 1 Then
			If Me.theSourceEngineModel.PhyFileHeader.theSourcePhyIsCollisionModel Then
				line = "$CollisionModel "
			Else
				line = "$CollisionJoints "
			End If
			'line += """phymodel.smd"""
			line += """"
			line += theSourceEngineModel.GetPhysicsSmdFileName()
			line += """"
			Me.theOutputFileStream.WriteLine(line)
			line = "{"
			Me.theOutputFileStream.WriteLine(line)

			Me.WriteCollisionModelOrCollisionJointsOptions()

			line = "}"
			Me.theOutputFileStream.WriteLine(line)
		End If
	End Sub

	'$animatedfriction
	'$assumeworldspace
	'$automass          // baked-in as mass
	'$concave           // done
	'$concaveperjoint
	'$damping           // done
	'$drag
	'$inertia           // done
	'$jointcollide
	'$jointconstrain    // done
	'$jointdamping      // done
	'$jointinertia      // done
	'$jointmassbias     // done
	'$jointmerge
	'$jointrotdamping   // done
	'$jointskip
	'$mass              // done
	'$masscenter
	'$maxconvexpieces
	'$noselfcollisions  //done
	'$remove2d
	'$rollingDrag
	'$rootbone          // done
	'$rotdamping        // done
	'$weldnormal
	'$weldposition
	Private Sub WriteCollisionModelOrCollisionJointsOptions()
		Dim line As String = ""

		line = vbTab
		line += "$mass "
		line += Me.theSourceEngineModel.PhyFileHeader.theSourcePhyEditParamsSection.totalMass.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStream.WriteLine(line)
		line = vbTab
		line += "$inertia "
		line += Me.theSourceEngineModel.PhyFileHeader.theSourcePhyPhysCollisionModelMostUsedValues.theInertia.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStream.WriteLine(line)
		line = vbTab
		line += "$damping "
		line += Me.theSourceEngineModel.PhyFileHeader.theSourcePhyPhysCollisionModelMostUsedValues.theDamping.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStream.WriteLine(line)
		line = vbTab
		line += "$rotdamping "
		line += Me.theSourceEngineModel.PhyFileHeader.theSourcePhyPhysCollisionModelMostUsedValues.theRotDamping.ToString("0.######", TheApp.InternalNumberFormat)
		Me.theOutputFileStream.WriteLine(line)
		If Me.theSourceEngineModel.PhyFileHeader.theSourcePhyEditParamsSection.rootName <> "" Then
			line = vbTab
			line += "$rootbone """
			line += Me.theSourceEngineModel.PhyFileHeader.theSourcePhyEditParamsSection.rootName
			line += """"
			Me.theOutputFileStream.WriteLine(line)
		End If
		If Me.theSourceEngineModel.PhyFileHeader.theSourcePhyEditParamsSection.concave = "1" Then
			line = vbTab
			line += "$concave"
			Me.theOutputFileStream.WriteLine(line)
		End If

		For i As Integer = 0 To Me.theSourceEngineModel.PhyFileHeader.theSourcePhyPhysCollisionModels.Count - 1
			Dim aSourcePhysCollisionModel As SourcePhyPhysCollisionModel
			aSourcePhysCollisionModel = Me.theSourceEngineModel.PhyFileHeader.theSourcePhyPhysCollisionModels(i)

			line = ""
			Me.theOutputFileStream.WriteLine(line)

			'If aSourcePhysCollisionModel.theDragCoefficientIsValid Then
			'End If

			If aSourcePhysCollisionModel.theMassBiasIsValid Then
				line = vbTab
				line += "$jointmassbias """
				line += aSourcePhysCollisionModel.theName
				line += """ "
				line += aSourcePhysCollisionModel.theMassBias.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStream.WriteLine(line)
			End If

			If aSourcePhysCollisionModel.theDamping <> Me.theSourceEngineModel.PhyFileHeader.theSourcePhyPhysCollisionModelMostUsedValues.theDamping Then
				line = vbTab
				line += "$jointdamping """
				line += aSourcePhysCollisionModel.theName
				line += """ "
				line += aSourcePhysCollisionModel.theDamping.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStream.WriteLine(line)
			End If

			If aSourcePhysCollisionModel.theInertia <> Me.theSourceEngineModel.PhyFileHeader.theSourcePhyPhysCollisionModelMostUsedValues.theInertia Then
				line = vbTab
				line += "$jointinertia """
				line += aSourcePhysCollisionModel.theName
				line += """ "
				line += aSourcePhysCollisionModel.theInertia.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStream.WriteLine(line)
			End If

			If aSourcePhysCollisionModel.theRotDamping <> Me.theSourceEngineModel.PhyFileHeader.theSourcePhyPhysCollisionModelMostUsedValues.theRotDamping Then
				line = vbTab
				line += "$jointrotdamping """
				line += aSourcePhysCollisionModel.theName
				line += """ "
				line += aSourcePhysCollisionModel.theRotDamping.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStream.WriteLine(line)
			End If

			If Me.theSourceEngineModel.PhyFileHeader.theSourcePhyRagdollConstraintDescs.ContainsKey(aSourcePhysCollisionModel.theIndex) Then
				Dim aConstraint As SourcePhyRagdollConstraint
				aConstraint = Me.theSourceEngineModel.PhyFileHeader.theSourcePhyRagdollConstraintDescs(aSourcePhysCollisionModel.theIndex)
				line = vbTab
				line += "$jointconstrain """
				line += aSourcePhysCollisionModel.theName
				line += """ x limit "
				line += aConstraint.theXMin.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aConstraint.theXMax.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aConstraint.theXFriction.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStream.WriteLine(line)
				line = vbTab
				line += "$jointconstrain """
				line += aSourcePhysCollisionModel.theName
				line += """ y limit "
				line += aConstraint.theYMin.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aConstraint.theYMax.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aConstraint.theYFriction.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStream.WriteLine(line)
				line = vbTab
				line += "$jointconstrain """
				line += aSourcePhysCollisionModel.theName
				line += """ z limit "
				line += aConstraint.theZMin.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aConstraint.theZMax.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aConstraint.theZFriction.ToString("0.######", TheApp.InternalNumberFormat)
				Me.theOutputFileStream.WriteLine(line)
			End If
		Next

		If Not Me.theSourceEngineModel.PhyFileHeader.theSourcePhySelfCollides Then
			line = vbTab
			line += "$noselfcollisions"
			Me.theOutputFileStream.WriteLine(line)
		Else
			For Each aSourcePhyCollisionPair As SourcePhyCollisionPair In Me.theSourceEngineModel.PhyFileHeader.theSourcePhyCollisionPairs
				line = vbTab
				line += "$jointcollide"
				line += " "
				line += """"
				line += Me.theSourceEngineModel.PhyFileHeader.theSourcePhyPhysCollisionModels(aSourcePhyCollisionPair.obj0).theName
				line += """"
				line += " "
				line += """"
				line += Me.theSourceEngineModel.PhyFileHeader.theSourcePhyPhysCollisionModels(aSourcePhyCollisionPair.obj1).theName
				line += """"
				Me.theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	Private Sub WriteCollisionTextCommand()
		Dim line As String = ""

		Try
			If Me.theSourceEngineModel.PhyFileHeader IsNot Nothing AndAlso Me.theSourceEngineModel.PhyFileHeader.theSourcePhyCollisionText IsNot Nothing AndAlso Me.theSourceEngineModel.PhyFileHeader.theSourcePhyCollisionText.Length > 0 Then
				line = ""
				Me.theOutputFileStream.WriteLine(line)

				line = "$CollisionText"
				Me.theOutputFileStream.WriteLine(line)

				line = "{"
				Me.theOutputFileStream.WriteLine(line)

				Me.WriteTextLines(theSourceEngineModel.PhyFileHeader.theSourcePhyCollisionText, 1)

				line = "}"
				Me.theOutputFileStream.WriteLine(line)
			End If
		Catch ex As Exception

		End Try
	End Sub

	Private Sub WriteGroupBone()
		Me.WriteDefineBoneCommand()
		Me.WriteBoneMergeCommand()

		Me.WriteProceduralBonesCommand()
		Me.WriteJiggleBoneCommand()
	End Sub

	Private Sub WriteDefineBoneCommand()
		If Not TheApp.Settings.DecompileQcIncludeDefineBoneLinesIsChecked Then
			Exit Sub
		End If

		Dim line As String = ""

		'NOTE: Should not be used with L4D2 survivors, because it messes up the mesh in animations.
		'TODO: Need to figure out when to insert the lines, such as is typical for L4D2 view models.

		'$definebone "ValveBiped.root" "" 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000
		If Me.theSourceEngineModel.MdlFileHeader.theBones IsNot Nothing Then
			Dim aBone As SourceMdlBone
			Dim aParentBoneName As String
			Dim aFixupPosition As New SourceVector()
			Dim aFixupRotation As New SourceVector()

			If Me.theSourceEngineModel.MdlFileHeader.theBones.Count > 0 Then
				Me.theOutputFileStream.WriteLine()
			End If

			For i As Integer = 0 To Me.theSourceEngineModel.MdlFileHeader.theBones.Count - 1
				aBone = Me.theSourceEngineModel.MdlFileHeader.theBones(i)
				If aBone.parentBoneIndex = -1 Then
					aParentBoneName = ""
				Else
					aParentBoneName = Me.theSourceEngineModel.MdlFileHeader.theBones(aBone.parentBoneIndex).theName
				End If

				line = "$DefineBone "
				line += """"
				line += aBone.theName
				line += """"
				line += " "
				line += """"
				line += aParentBoneName
				line += """"

				line += " "
				line += aBone.position.x.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aBone.position.y.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aBone.position.z.ToString("0.######", TheApp.InternalNumberFormat)

				If Me.theSourceEngineModel.MdlFileHeader.version = 2531 Then
					line += " 0.000000 0.000000 0.000000"
				Else
					line += " "
					line += MathModule.RadiansToDegrees(aBone.rotation.y).ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += MathModule.RadiansToDegrees(aBone.rotation.z).ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += MathModule.RadiansToDegrees(aBone.rotation.x).ToString("0.######", TheApp.InternalNumberFormat)
				End If

				'TODO: These fixups are all zeroes for now.
				'      They might be found in the srcbonetransform list.
				'      Note the g_bonetable[nParent].srcRealign that seems linked to the input fixup values of $definebone.
				'FROM: write.cpp
				'mstudiosrcbonetransform_t *pSrcBoneTransform = (mstudiosrcbonetransform_t *)pData;
				'phdr->numsrcbonetransform = nTransformCount;
				'phdr->srcbonetransformindex = pData - pStart;
				'pData += nTransformCount * sizeof( mstudiosrcbonetransform_t );
				'int bt = 0;
				'for ( int i = 0; i < g_numbones; i++ )
				'{
				'	if ( g_bonetable[i].flags & BONE_ALWAYS_PROCEDURAL )
				'		continue;
				'	int nParent = g_bonetable[i].parent;
				'	if ( MatricesAreEqual( identity, g_bonetable[i].srcRealign ) &&
				'		( ( nParent < 0 ) || MatricesAreEqual( identity, g_bonetable[nParent].srcRealign ) ) )
				'		continue;

				'	// What's going on here?
				'	// So, when we realign a bone, we want to do it in a way so that the child bones
				'	// have the same bone->world transform. If we take T as the src realignment transform
				'	// for the parent, P is the parent to world, and C is the child to parent, we expect 
				'	// the child->world is constant after realignment:
				'	//		CtoW = P * C = ( P * T ) * ( T^-1 * C )
				'	// therefore Cnew = ( T^-1 * C )
				'						If (nParent >= 0) Then
				'	{
				'		MatrixInvert( g_bonetable[nParent].srcRealign, pSrcBoneTransform[bt].pretransform );
				'	}
				'						Else
				'	{
				'		SetIdentityMatrix( pSrcBoneTransform[bt].pretransform );
				'	}
				'	MatrixCopy( g_bonetable[i].srcRealign, pSrcBoneTransform[bt].posttransform );
				'	AddToStringTable( &pSrcBoneTransform[bt], &pSrcBoneTransform[bt].sznameindex, g_bonetable[i].name );
				'	++bt;
				'}

				line += " "
				line += aFixupPosition.x.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aFixupPosition.y.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aFixupPosition.z.ToString("0.######", TheApp.InternalNumberFormat)

				line += " "
				line += aFixupRotation.x.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aFixupRotation.y.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aFixupRotation.z.ToString("0.######", TheApp.InternalNumberFormat)

				Me.theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	Private Sub WriteProceduralBonesCommand()
		'$proceduralbones "proceduralbones.vrd"
		If Me.theSourceEngineModel.MdlFileHeader.theProceduralBonesCommandIsUsed Then
			Me.theOutputFileStream.WriteLine()

			Dim line As String = ""
			line += "$ProceduralBones "
			line += """"
			line += Me.theSourceEngineModel.GetVrdFileName()
			line += """"
			Me.theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteBoneMergeCommand()
		Dim line As String = ""

		'$bonemerge "ValveBiped.Bip01_R_Hand"
		If Me.theSourceEngineModel.MdlFileHeader.theBones IsNot Nothing Then
			Dim aBone As SourceMdlBone
			Dim emptyLineIsAlreadyWritten As Boolean

			emptyLineIsAlreadyWritten = False
			For i As Integer = 0 To theSourceEngineModel.MdlFileHeader.theBones.Count - 1
				aBone = Me.theSourceEngineModel.MdlFileHeader.theBones(i)

				If (aBone.flags And SourceMdlBone.BONE_USED_BY_BONE_MERGE) > 0 Then
					If Not emptyLineIsAlreadyWritten Then
						Me.theOutputFileStream.WriteLine()
						emptyLineIsAlreadyWritten = True
					End If

					line = "$BoneMerge "
					line += """"
					line += aBone.theName
					line += """"
					Me.theOutputFileStream.WriteLine(line)
				End If
			Next
		End If
	End Sub

	Private Sub WriteJiggleBoneCommand()
		If theSourceEngineModel.MdlFileHeader.theBones Is Nothing Then
			Return
		End If

		Dim line As String = ""

		line = ""
		Me.theOutputFileStream.WriteLine(line)

		For i As Integer = 0 To Me.theSourceEngineModel.MdlFileHeader.theBones.Count - 1
			Dim aBone As SourceMdlBone
			aBone = Me.theSourceEngineModel.MdlFileHeader.theBones(i)
			If aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_JIGGLE AndAlso aBone.proceduralRuleOffset <> 0 Then
				line = "$JiggleBone "
				line += """"
				line += aBone.theName
				line += """"
				Me.theOutputFileStream.WriteLine(line)
				line = "{"
				Me.theOutputFileStream.WriteLine(line)
				If (aBone.theJiggleBone.flags And SourceMdlJiggleBone.JIGGLE_IS_FLEXIBLE) > 0 Then
					line = vbTab
					line += "is_flexible"
					Me.theOutputFileStream.WriteLine(line)
					line = vbTab
					line += "{"
					Me.theOutputFileStream.WriteLine(line)

					line = vbTab
					line += vbTab
					line += "length "
					line += aBone.theJiggleBone.length.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "tip_mass "
					line += aBone.theJiggleBone.tipMass.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)

					line = vbTab
					line += vbTab
					line += "pitch_stiffness "
					line += aBone.theJiggleBone.pitchStiffness.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "pitch_damping "
					line += aBone.theJiggleBone.pitchDamping.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "yaw_stiffness "
					line += aBone.theJiggleBone.yawStiffness.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "yaw_damping "
					line += aBone.theJiggleBone.yawDamping.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)

					If (aBone.theJiggleBone.flags And SourceMdlJiggleBone.JIGGLE_HAS_LENGTH_CONSTRAINT) > 0 Then
						line = vbTab
						line += vbTab
						line += "allow_length_flex"
						Me.theOutputFileStream.WriteLine(line)
					End If
					line = vbTab
					line += vbTab
					line += "along_stiffness "
					line += aBone.theJiggleBone.alongStiffness.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "along_damping "
					line += aBone.theJiggleBone.alongDamping.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)

					Me.WriteJiggleBoneConstraints(aBone)

					line = vbTab
					line += "}"
					Me.theOutputFileStream.WriteLine(line)
				End If
				If (aBone.theJiggleBone.flags And SourceMdlJiggleBone.JIGGLE_IS_RIGID) > 0 Then
					line = vbTab
					line += "is_rigid"
					Me.theOutputFileStream.WriteLine(line)
					line = vbTab
					line += "{"
					Me.theOutputFileStream.WriteLine(line)

					line = vbTab
					line += vbTab
					line += "length "
					line += aBone.theJiggleBone.length.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "tip_mass "
					line += aBone.theJiggleBone.tipMass.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)

					Me.WriteJiggleBoneConstraints(aBone)

					line = vbTab
					line += "}"
					Me.theOutputFileStream.WriteLine(line)
				End If
				If (aBone.theJiggleBone.flags And SourceMdlJiggleBone.JIGGLE_HAS_BASE_SPRING) > 0 Then
					line = vbTab
					line += "has_base_spring"
					Me.theOutputFileStream.WriteLine(line)
					line = vbTab
					line += "{"
					Me.theOutputFileStream.WriteLine(line)

					line = vbTab
					line += vbTab
					line += "base_mass "
					line += aBone.theJiggleBone.baseMass.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "stiffness "
					line += aBone.theJiggleBone.baseStiffness.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "damping "
					line += aBone.theJiggleBone.baseDamping.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)

					line = vbTab
					line += vbTab
					line += "left_constraint "
					'line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMinLeft).ToString("0.######", TheApp.InternalNumberFormat)
					'line += " "
					'line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMaxLeft).ToString("0.######", TheApp.InternalNumberFormat)
					line += aBone.theJiggleBone.baseMinLeft.ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += aBone.theJiggleBone.baseMaxLeft.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "left_friction "
					line += aBone.theJiggleBone.baseLeftFriction.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)

					line = vbTab
					line += vbTab
					line += "up_constraint "
					'line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMinUp).ToString("0.######", TheApp.InternalNumberFormat)
					'line += " "
					'line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMaxUp).ToString("0.######", TheApp.InternalNumberFormat)
					line += aBone.theJiggleBone.baseMinUp.ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += aBone.theJiggleBone.baseMaxUp.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "up_friction "
					line += aBone.theJiggleBone.baseUpFriction.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)

					line = vbTab
					line += vbTab
					line += "forward_constraint "
					'line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMinForward).ToString("0.######", TheApp.InternalNumberFormat)
					'line += " "
					'line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMaxForward).ToString("0.######", TheApp.InternalNumberFormat)
					line += aBone.theJiggleBone.baseMinForward.ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += aBone.theJiggleBone.baseMaxForward.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)
					line = vbTab
					line += vbTab
					line += "forward_friction "
					line += aBone.theJiggleBone.baseForwardFriction.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)

					line = vbTab
					line += "}"
					Me.theOutputFileStream.WriteLine(line)
				End If
				line = "}"
				Me.theOutputFileStream.WriteLine(line)
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
			Me.theOutputFileStream.WriteLine(line)
			line = vbTab
			line += vbTab
			line += "pitch_friction "
			line += aBone.theJiggleBone.pitchFriction.ToString("0.######", TheApp.InternalNumberFormat)
			Me.theOutputFileStream.WriteLine(line)
			line = vbTab
			line += vbTab
			line += "pitch_bounce "
			line += aBone.theJiggleBone.pitchBounce.ToString("0.######", TheApp.InternalNumberFormat)
			Me.theOutputFileStream.WriteLine(line)
		End If

		If (aBone.theJiggleBone.flags And SourceMdlJiggleBone.JIGGLE_HAS_YAW_CONSTRAINT) > 0 Then
			line = vbTab
			line += vbTab
			line += "yaw_constraint "
			line += MathModule.RadiansToDegrees(aBone.theJiggleBone.minYaw).ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += MathModule.RadiansToDegrees(aBone.theJiggleBone.maxYaw).ToString("0.######", TheApp.InternalNumberFormat)
			Me.theOutputFileStream.WriteLine(line)
			line = vbTab
			line += vbTab
			line += "yaw_friction "
			line += aBone.theJiggleBone.yawFriction.ToString("0.######", TheApp.InternalNumberFormat)
			Me.theOutputFileStream.WriteLine(line)
			line = vbTab
			line += vbTab
			line += "yaw_bounce "
			line += aBone.theJiggleBone.yawBounce.ToString("0.######", TheApp.InternalNumberFormat)
			Me.theOutputFileStream.WriteLine(line)
		End If

		If (aBone.theJiggleBone.flags And SourceMdlJiggleBone.JIGGLE_HAS_ANGLE_CONSTRAINT) > 0 Then
			line = vbTab
			line += vbTab
			line += "angle_constraint "
			line += MathModule.RadiansToDegrees(aBone.theJiggleBone.angleLimit).ToString("0.######", TheApp.InternalNumberFormat)
			Me.theOutputFileStream.WriteLine(line)
		End If
	End Sub

	Private Sub WriteGroupBox()
		Me.WriteCBoxCommand()
		Me.WriteBBoxCommand()
		If Me.theSourceEngineModel.MdlFileHeader.theHitboxSets IsNot Nothing Then
			If Me.theSourceEngineModel.MdlFileHeader.version <= 10 Then
				Dim skipBoneInBBoxCommandWasUsed As Boolean = False
				Me.theOutputFileStream.WriteLine()
				Me.WriteHboxCommands(Me.theSourceEngineModel.MdlFileHeader.theHitboxSets(0).theHitboxes, "", "", skipBoneInBBoxCommandWasUsed)
			Else
				Me.WriteHBoxRelatedCommands()
			End If
		End If
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
		Me.theOutputFileStream.WriteLine(line)

		If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
			line = "// Clipping box or view bounding box."
			Me.theOutputFileStream.WriteLine(line)
		End If

		'FROM: VDC wiki: 
		'$cbox <float|minx> <float|miny> <float|minz> <float|maxx> <float|maxy> <float|maxz> 
		minX = Math.Round(theSourceEngineModel.MdlFileHeader.viewBoundingBoxMinPositionX, 3)
		minY = Math.Round(theSourceEngineModel.MdlFileHeader.viewBoundingBoxMinPositionY, 3)
		minZ = Math.Round(theSourceEngineModel.MdlFileHeader.viewBoundingBoxMinPositionZ, 3)
		maxX = Math.Round(theSourceEngineModel.MdlFileHeader.viewBoundingBoxMaxPositionX, 3)
		maxY = Math.Round(theSourceEngineModel.MdlFileHeader.viewBoundingBoxMaxPositionY, 3)
		maxZ = Math.Round(theSourceEngineModel.MdlFileHeader.viewBoundingBoxMaxPositionZ, 3)
		line = "$CBox "
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
		Me.theOutputFileStream.WriteLine(line)
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
		Me.theOutputFileStream.WriteLine(line)

		If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
			line = "// Bounding box or hull. Used for collision with a world object."
			Me.theOutputFileStream.WriteLine(line)
		End If

		'$bbox -16.0 -16.0 -13.0 16.0 16.0 75.0
		'FROM: VDC wiki: 
		'$bbox (min x) (min y) (min z) (max x) (max y) (max z)
		minX = Math.Round(theSourceEngineModel.MdlFileHeader.hullMinPositionX, 3)
		minY = Math.Round(theSourceEngineModel.MdlFileHeader.hullMinPositionY, 3)
		minZ = Math.Round(theSourceEngineModel.MdlFileHeader.hullMinPositionZ, 3)
		maxX = Math.Round(theSourceEngineModel.MdlFileHeader.hullMaxPositionX, 3)
		maxY = Math.Round(theSourceEngineModel.MdlFileHeader.hullMaxPositionY, 3)
		maxZ = Math.Round(theSourceEngineModel.MdlFileHeader.hullMaxPositionZ, 3)
		line = ""
		line += "$BBox "
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
		Me.theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteHBoxRelatedCommands()
		Dim line As String = ""
		Dim commentTag As String = ""
		Dim hitBoxWasAutoGenerated As Boolean = False
		Dim skipBoneInBBoxCommandWasUsed As Boolean = False

		If Me.theSourceEngineModel.MdlFileHeader.theHitboxSets.Count < 1 Then
			Exit Sub
		End If

		hitBoxWasAutoGenerated = (Me.theSourceEngineModel.MdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_AUTOGENERATED_HITBOX) > 0
		If hitBoxWasAutoGenerated AndAlso Not TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
			Exit Sub
		End If

		Me.theOutputFileStream.WriteLine()

		If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
			line = "// Hitbox info. Used for damage-based collision."
			Me.theOutputFileStream.WriteLine(line)
		End If

		If hitBoxWasAutoGenerated AndAlso TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
			line = "// The hitbox info below was automatically generated when compiled because no hitbox info was provided."
			Me.theOutputFileStream.WriteLine(line)
		End If

		'TODO: Maybe make a checkbox option for whether to write lines or not instead of using comments.
		'NOTE: Always comment-out the hbox lines if in main QC file.
		If Not TheApp.Settings.DecompileGroupIntoQciFilesIsChecked Then
			commentTag = "// "
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
		For i As Integer = 0 To Me.theSourceEngineModel.MdlFileHeader.theHitboxSets.Count - 1
			aHitboxSet = Me.theSourceEngineModel.MdlFileHeader.theHitboxSets(i)

			line = "$HBoxSet "
			line += """"
			line += aHitboxSet.theName
			line += """"
			Me.theOutputFileStream.WriteLine(commentTag + line)

			If aHitboxSet.theHitboxes Is Nothing Then
				Continue For
			End If

			Me.WriteHboxCommands(aHitboxSet.theHitboxes, commentTag, aHitboxSet.theName, skipBoneInBBoxCommandWasUsed)
		Next

		If skipBoneInBBoxCommandWasUsed Then
			line = "$SkipBoneInBBox"
			Me.theOutputFileStream.WriteLine(commentTag + line)
		End If
	End Sub

	Private Sub WriteHboxCommands(ByVal theHitboxes As List(Of SourceMdlHitbox), ByVal commentTag As String, ByVal hitboxSetName As String, ByRef theSkipBoneInBBoxCommandWasUsed As Boolean)
		Dim line As String = ""
		Dim aHitbox As SourceMdlHitbox

		For j As Integer = 0 To theHitboxes.Count - 1
			aHitbox = theHitboxes(j)
			line = "$HBox "
			line += aHitbox.groupIndex.ToString(TheApp.InternalNumberFormat)
			line += " "
			line += """"
			line += Me.theSourceEngineModel.MdlFileHeader.theBones(aHitbox.boneIndex).theName
			line += """"
			line += " "
			line += aHitbox.boundingBoxMin.x.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += aHitbox.boundingBoxMin.y.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += aHitbox.boundingBoxMin.z.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += aHitbox.boundingBoxMax.x.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += aHitbox.boundingBoxMax.y.ToString("0.######", TheApp.InternalNumberFormat)
			line += " "
			line += aHitbox.boundingBoxMax.z.ToString("0.######", TheApp.InternalNumberFormat)
			'NOTE: For L4D2 survivor_teenangst, the extra zeroes cause this compile error: 
			'ERROR: c:\users\zeqmacaw\documents\- unpacked source\left 4 dead 2\left4dead2_dlc3\models\survivors\decompiled 0.26\survivor_teenangst\survivor_teenangst_boxes.qci(10): - bad command 0
			'ERROR: Aborted Processing on 'survivors/survivor_TeenAngst.mdl'
			'If Me.theSourceEngineModel.theMdlFileHeader.version >= 49 Then
			'TODO: [WriteHboxCommands] Probably need better way to determine when to write extra values.
			If Me.theSourceEngineModel.MdlFileHeader.version >= 49 AndAlso hitboxSetName = "cstrike" Then
				'NOTE: Roll (z) is first.
				line += " "
				line += aHitbox.boundingBoxPitchYawRoll.z.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aHitbox.boundingBoxPitchYawRoll.x.ToString("0.######", TheApp.InternalNumberFormat)
				line += " "
				line += aHitbox.boundingBoxPitchYawRoll.y.ToString("0.######", TheApp.InternalNumberFormat)
			End If
			Me.theOutputFileStream.WriteLine(commentTag + line)

			If Not theSkipBoneInBBoxCommandWasUsed Then
				If aHitbox.boundingBoxMin.x > 0 _
				 OrElse aHitbox.boundingBoxMin.y > 0 _
				 OrElse aHitbox.boundingBoxMin.z > 0 _
				 OrElse aHitbox.boundingBoxMax.x < 0 _
				 OrElse aHitbox.boundingBoxMax.y < 0 _
				 OrElse aHitbox.boundingBoxMax.z < 0 _
				 Then
					theSkipBoneInBBoxCommandWasUsed = True
				End If
			End If
		Next
	End Sub

	Private Sub WriteBodyGroupCommand(ByVal startIndex As Integer)
		Dim line As String = ""
		Dim aBodyPart As SourceMdlBodyPart
		Dim aVtxBodyPart As SourceVtxBodyPart
		Dim aModel As SourceMdlModel
		Dim aVtxModel As SourceVtxModel

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
		If theSourceEngineModel.MdlFileHeader.theBodyParts IsNot Nothing AndAlso theSourceEngineModel.MdlFileHeader.theBodyParts.Count > startIndex Then
			line = ""
			Me.theOutputFileStream.WriteLine(line)

			'NOTE: Skip the first because $body(?) and $model handle that one.
			For bodyPartIndex As Integer = startIndex To theSourceEngineModel.MdlFileHeader.theBodyParts.Count - 1
				aBodyPart = theSourceEngineModel.MdlFileHeader.theBodyParts(bodyPartIndex)
				aVtxBodyPart = theSourceEngineModel.VtxFileHeader.theVtxBodyParts(bodyPartIndex)

				line = "$BodyGroup "
				line += """"
				line += aBodyPart.theName
				line += """"
				Me.theOutputFileStream.WriteLine(line)

				line = "{"
				Me.theOutputFileStream.WriteLine(line)

				If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
					For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
						aModel = aBodyPart.theModels(modelIndex)
						aVtxModel = aVtxBodyPart.theVtxModels(modelIndex)

						line = vbTab
						'If aModel.name(0) = ChrW(0) Then
						If aModel.name(0) = ChrW(0) AndAlso aVtxModel.theVtxModelLods(0).theVtxMeshes Is Nothing Then
							line += "blank"
						Else
							line += "studio "
							line += """"
							line += Me.theSourceEngineModel.GetBodyGroupSmdFileName(bodyPartIndex, modelIndex, 0)
							line += """"
						End If
						Me.theOutputFileStream.WriteLine(line)
					Next
				End If

				line = "}"
				Me.theOutputFileStream.WriteLine(line)
			Next
		End If
	End Sub

	Private Sub WriteControllerCommand()
		Dim line As String = ""
		Dim boneController As SourceMdlBoneController

		'$controller mouth "jaw" X 0 20
		'$controller 0 "tracker" LYR -1 1
		Try
			If Me.theSourceEngineModel.MdlFileHeader.theBoneControllers IsNot Nothing Then
				If Me.theSourceEngineModel.MdlFileHeader.theBoneControllers.Count > 0 Then
					Me.theOutputFileStream.WriteLine()
				End If

				For i As Integer = 0 To Me.theSourceEngineModel.MdlFileHeader.theBoneControllers.Count - 1
					boneController = Me.theSourceEngineModel.MdlFileHeader.theBoneControllers(i)

					line = "$Controller "
					line += boneController.inputField.ToString(TheApp.InternalNumberFormat)
					line += " """
					line += Me.theSourceEngineModel.MdlFileHeader.theBones(boneController.boneIndex).theName
					line += """ "
					line += boneController.TypeName
					line += " "
					line += boneController.startBlah.ToString("0.######", TheApp.InternalNumberFormat)
					line += " "
					line += boneController.endBlah.ToString("0.######", TheApp.InternalNumberFormat)
					Me.theOutputFileStream.WriteLine(line)
				Next
			End If
		Catch ex As Exception

		End Try
	End Sub

	Private Sub WriteScreenAlignCommand()
		Dim line As String = ""

		'$screenalign <bone name> <"sphere" or "cylinder">
		Try
			If Me.theSourceEngineModel.MdlFileHeader.theBones IsNot Nothing Then
				Dim aBone As SourceMdlBone
				Dim emptyLineIsAlreadyWritten As Boolean

				emptyLineIsAlreadyWritten = False
				For i As Integer = 0 To Me.theSourceEngineModel.MdlFileHeader.theBones.Count - 1
					aBone = Me.theSourceEngineModel.MdlFileHeader.theBones(i)

					If (aBone.flags And SourceMdlBone.BONE_SCREEN_ALIGN_SPHERE) > 0 Then
						If Not emptyLineIsAlreadyWritten Then
							Me.theOutputFileStream.WriteLine()
							emptyLineIsAlreadyWritten = True
						End If

						line = "$ScreenAlign "
						line += aBone.theName
						line += " ""sphere"""
						Me.theOutputFileStream.WriteLine(line)
					ElseIf (aBone.flags And SourceMdlBone.BONE_SCREEN_ALIGN_CYLINDER) > 0 Then
						If Not emptyLineIsAlreadyWritten Then
							Me.theOutputFileStream.WriteLine()
							emptyLineIsAlreadyWritten = True
						End If

						line = "$ScreenAlign "
						line += aBone.theName
						line += " ""cylinder"""
						Me.theOutputFileStream.WriteLine(line)
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
                Me.theOutputFileStream.WriteLine(line)

                line = commandOrOptionText
                Me.theOutputFileStream.WriteLine(line)

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
                '			Me.theOutputFileStream.WriteLine(line)
                '		End If

                '		line = "{"
                '		lengthToRemove = stopIndex + openBraceText.Length
                '	Else
                '		stopIndex = text.IndexOf(closeBraceText)
                '		If stopIndex > -1 Then
                '			If stopIndex > 0 Then
                '				line = text.Substring(0, stopIndex)
                '				Me.theOutputFileStream.WriteLine(line)
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
                '	Me.theOutputFileStream.WriteLine(line)

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
                    Me.theOutputFileStream.WriteLine(line)
                End If

                line = indentText
                line += "{"
                Me.theOutputFileStream.WriteLine(line)

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
                        Me.theOutputFileStream.WriteLine(line)
                    End If
                End If

                indentCount -= 1
                indentText = ""
                For j As Integer = 1 To indentCount
                    indentText += vbTab
                Next

                line = indentText
                line += "}"
                Me.theOutputFileStream.WriteLine(line)

                startIndex = i + 1
                lineQuoteCount = 0
            ElseIf textChar = """" Then
                lineQuoteCount += 1
                If lineQuoteCount = 4 Then
                    If i > startIndex Then
                        line = indentText
                        line += text.Substring(startIndex, i - startIndex + 1).Trim()
                        Me.theOutputFileStream.WriteLine(line)
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
    Private theOutputPathName As String
    Private theOutputFileNameWithoutExtension As String

    'Private theFirstAnimationDescName As String

#End Region

End Class
