Imports System.IO
Imports System.Text

Public Class SourceVrdFile

#Region "Methods"

	Public Sub WriteFile(ByVal pathFileName As String, ByVal aSourceEngineModel As SourceModel)
		'Dim outputPathFileName As String

		'outputPathFileName = Path.ChangeExtension(pathFileName, ".vrd")
		'outputPathFileName = Path.Combine(FileManager.GetPath(pathFileName), "proceduralbones.vrd")
		Me.theOutputFileStream = File.CreateText(pathFileName)

		Me.theSourceEngineModel = aSourceEngineModel

		Me.WriteHeaderComment()

		Me.WriteCommands()

		theOutputFileStream.Flush()
		theOutputFileStream.Close()
	End Sub

#End Region

#Region "Private Methods"

	Private Sub WriteHeaderComment()
		Dim line As String = ""

		line = "// "
		line += TheApp.GetHeaderComment()
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteCommands()
		If theSourceEngineModel.MdlFileHeader.theBones IsNot Nothing Then
			Dim line As String = ""
			Dim aBone As SourceMdlBone
			Dim aParentBone As SourceMdlBone
			Dim aControlBone As SourceMdlBone
			Dim aParentControlBone As SourceMdlBone
			Dim aTrigger As SourceMdlQuatInterpBoneInfo
			Dim aTriggerTrigger As SourceVector
			Dim aTriggerQuat As SourceVector

			For i As Integer = 0 To theSourceEngineModel.MdlFileHeader.theBones.Count - 1
				aBone = theSourceEngineModel.MdlFileHeader.theBones(i)

				If aBone.proceduralRuleOffset <> 0 Then
					If aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_AXISINTERP Then
					ElseIf aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_QUATINTERP Then
						'<helper> Bip01_L_Elbow Bip01_L_UpperArm Bip01_L_UpperArm Bip01_L_Forearm
						'<display> 1.5 3 3 100
						'<basepos> 0 0 0
						'<trigger> 90 0 0 0 0 0 0 0 0 0
						'<trigger> 90 0 0 -90 0 0 -45 0 0 0

						'int i = sscanf( g_szLine, "%s %s %s %s %s", cmd, pBone->bonename, pBone->parentname, pBone->controlparentname, pBone->controlname );
						aParentBone = theSourceEngineModel.MdlFileHeader.theBones(aBone.parentBoneIndex)
						aControlBone = theSourceEngineModel.MdlFileHeader.theBones(aBone.theQuatInterpBone.controlBoneIndex)
						aParentControlBone = theSourceEngineModel.MdlFileHeader.theBones(aControlBone.parentBoneIndex)

						theOutputFileStream.WriteLine()

						line = "<helper>"
						line += " "
						line += aBone.theName.Replace("ValveBiped.", "")
						line += " "
						line += aParentBone.theName.Replace("ValveBiped.", "")
						line += " "
						line += aParentControlBone.theName.Replace("ValveBiped.", "")
						line += " "
						line += aControlBone.theName.Replace("ValveBiped.", "")
						theOutputFileStream.WriteLine(line)

						''NOTE: Use "1" for the 3 size values because it looks like they are not used in compile.
						'line = "<display>"
						'line += " "
						'line += "1"
						'line += " "
						'line += "1"
						'line += " "
						'line += "1"
						'line += " "
						''TODO: Reverse this to decompile.
						''pAxis->percentage = distance / 100.0;
						''tmp = pInterp->pos[k] + pInterp->basepos + g_bonetable[pInterp->control].pos * pInterp->percentage;
						'line += "100"
						'theOutputFileStream.WriteLine(line)

						line = "<basepos>"
						line += " "
						line += "0"
						line += " "
						line += "0"
						line += " "
						line += "0"
						theOutputFileStream.WriteLine(line)

						For triggerIndex As Integer = 0 To aBone.theQuatInterpBone.theTriggers.Count - 1
							aTrigger = aBone.theQuatInterpBone.theTriggers(triggerIndex)

							aTriggerTrigger = MathModule.ToEulerAngles(aTrigger.trigger)
							aTriggerQuat = MathModule.ToEulerAngles(aTrigger.quat)

							line = "<trigger>"
							line += " "
							'pAxis->tolerance[j] = DEG2RAD( tolerance );
							line += MathModule.RadiansToDegrees(1 / aTrigger.inverseToleranceAngle).ToString("0.######", TheApp.InternalNumberFormat)

							'trigger.x = DEG2RAD( trigger.x );
							'trigger.y = DEG2RAD( trigger.y );
							'trigger.z = DEG2RAD( trigger.z );
							'AngleQuaternion( trigger, pAxis->trigger[j] );
							line += " "
							line += MathModule.RadiansToDegrees(aTriggerTrigger.x).ToString("0.######", TheApp.InternalNumberFormat)
							line += " "
							line += MathModule.RadiansToDegrees(aTriggerTrigger.y).ToString("0.######", TheApp.InternalNumberFormat)
							line += " "
							line += MathModule.RadiansToDegrees(aTriggerTrigger.z).ToString("0.######", TheApp.InternalNumberFormat)
							'line += " "
							'line += MathModule.RadiansToDegrees(aTriggerTrigger.z).ToString("0.######", TheApp.InternalNumberFormat)
							'line += " "
							'line += MathModule.RadiansToDegrees(aTriggerTrigger.y).ToString("0.######", TheApp.InternalNumberFormat)
							'line += " "
							'line += MathModule.RadiansToDegrees(aTriggerTrigger.x).ToString("0.######", TheApp.InternalNumberFormat)

							'ang.x = DEG2RAD( ang.x );
							'ang.y = DEG2RAD( ang.y );
							'ang.z = DEG2RAD( ang.z );
							'AngleQuaternion( ang, pAxis->quat[j] );
							line += " "
							line += MathModule.RadiansToDegrees(aTriggerQuat.x).ToString("0.######", TheApp.InternalNumberFormat)
							line += " "
							line += MathModule.RadiansToDegrees(aTriggerQuat.y).ToString("0.######", TheApp.InternalNumberFormat)
							line += " "
							line += MathModule.RadiansToDegrees(aTriggerQuat.z).ToString("0.######", TheApp.InternalNumberFormat)
							'line += " "
							'line += MathModule.RadiansToDegrees(aTriggerQuat.z).ToString("0.######", TheApp.InternalNumberFormat)
							'line += " "
							'line += MathModule.RadiansToDegrees(aTriggerQuat.y).ToString("0.######", TheApp.InternalNumberFormat)
							'line += " "
							'line += MathModule.RadiansToDegrees(aTriggerQuat.x).ToString("0.######", TheApp.InternalNumberFormat)

							'VectorAdd( basepos, pos, pAxis->pos[j] );
							line += " "
							line += aTrigger.pos.x.ToString("0.######", TheApp.InternalNumberFormat)
							line += " "
							line += aTrigger.pos.y.ToString("0.######", TheApp.InternalNumberFormat)
							line += " "
							line += aTrigger.pos.z.ToString("0.######", TheApp.InternalNumberFormat)
							theOutputFileStream.WriteLine(line)
						Next
					End If
				End If
			Next
		End If
	End Sub

#End Region

#Region "Data"

	Private theSourceEngineModel As SourceModel
	Private theOutputFileStream As StreamWriter

#End Region

End Class
