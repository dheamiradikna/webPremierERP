﻿Imports [class].clsWebGeneral
Imports [class].clsGeneral
Imports [class].cls_wmContent
Imports [class].clsGeneralDB
Imports [class].clsUploadItem
Imports System.Data.SqlTypes
Imports System.Data
Imports Newtonsoft.Json

Partial Class wf_contentTagPopup
    Inherits System.Web.UI.Page

    Protected rootPath As String = _rootPath

    Protected type As String = ""
    Protected action As String = ""
    'Protected tagRef As String = ""


    Protected selContentType As String = ""
    Protected txtProjectName As String = ""
    Protected selImageSetting As String = ""
    Protected selImagePosition As String = ""
    Protected txtMetaTitle As String = ""
    Protected txtMetaAuthor As String = ""
    Protected txtKeyword As String = ""
    Protected txtMetaDescription As String = ""
    Protected txtVideo As String = ""
    Protected txtTitle As String = ""
    Protected txtTitleDetail As String = ""
    Protected txtSynopsis As String = ""
    Protected txtSynopsisNNP As String = ""
    Protected txtContent As String = ""
    


    Protected selDayContent As String = ""
    Protected selMonthContent As String = ""
    Protected txtYearContent As String = ""

    Protected txtContentDate As String = ""


    Protected txtMapLongitude As String = ""
    Protected txtMapLatitude As String = ""

    Protected txtMap As String = ""
    Protected synopsis As String = ""

    Protected txtSortNo As String = ""

    Protected selDayPublish As String = ""
    Protected selMonthPublish As String = ""
    Protected txtYearPublish As String = ""

    Protected selDayExpired As String = ""
    Protected selMonthExpired As String = ""
    Protected txtYearExpired As String = ""

    Protected isUpdate As String = ""
    Protected isTitle As String = ""
    Protected isTitleDetail As String = ""
    Protected isSynopsis As String = ""
    Protected isSynopsisNNP As String = ""
    Protected isContent As String = ""

    Protected isMap As String = ""

    Protected isThumbnail As String = ""
    Protected isPicture As String = ""
    Protected isVideo As String = ""
    Protected isAttachment As String = ""
    Protected isDate As String = ""
    Protected isPublishDate As String = ""
    Protected isExpiredDate As String = ""

    Protected isPB As String = "0"

    Protected tagRef As String = ""


    Protected _fileImgName As String = ""
    Protected _fileImgByte As String = ""
    Protected _fileImgUrl As String = ""
    Protected ImgBytes As Byte() = {0}
    Protected ImgFileRef As String = ""

    Protected domainRef As String = ""
    Protected imgRef As String = ""
    Protected contentRef As String = ""

    Protected urlBack As String = "#"
    Protected txtProjectRef As String = ""
    Protected _jsonContentRelated As String = "[]"

    'Protected txtComment As String = ""
    'Protected strLocation As String = ""
    'Protected strDevelopment As String = ""
    'Protected strMarketing As String = ""
    'Protected strManagement As String = ""




    Private Function bindDay(ByVal ctrlName As String, ByVal value As String) As String
        Dim result As New StringBuilder
        Dim i As Integer

        result.Append("<select id=""selDay" + ctrlName + """ name=""selDay" + ctrlName + """ onchange=""cekDate('" + ctrlName + "', document.getElementById('eDate" + ctrlName + "'));"">")
        result.Append("<option value=""-"">-</option>")
        For i = 1 To 31
            If value = i.ToString Then
                result.Append("<option selected=""selected"" value=""" + i.ToString + """>" + i.ToString + "</option>")
            Else
                result.Append("<option value=""" + i.ToString + """>" + i.ToString + "</option>")
            End If
        Next
        result.Append("</select>")

        Return result.ToString
    End Function

    Private Function bindMonth(ByVal ctrlName As String, ByVal value As String) As String
        Dim result As New StringBuilder
        Dim i As Integer

        result.Append("<select id=""selMonth" + ctrlName + """ name=""selMonth" + ctrlName + """ onchange=""cekDate('" + ctrlName + "', document.getElementById('eDate" + ctrlName + "'));"">")
        result.Append("<option value=""-"">-</option>")
        For i = 0 To monthValue.Count - 1
            If value = (i + 1).ToString Then
                result.Append(" <option selected=""selected"" value=""" + monthValue(i) + """>" + monthName(i) + "</option>")
            Else
                result.Append(" <option value=""" + monthValue(i) + """>" + monthName(i) + "</option>")
            End If
        Next

        result.Append("</select>")

        Return result.ToString
    End Function

    Private Function bindSelContentType(ByVal value As String) As String
        Dim result As New StringBuilder
        Dim i As Integer
        Dim dt As New DataTable

        dt = getContentTypeListLookup()
        If dt.Rows.Count > 0 Then
            result.Append("<select id=""selContentType"" name=""selContentType"" >")
            For i = 0 To dt.Rows.Count - 1

                If value = dt.Rows(i).Item("contentType").ToString Then
                    result.Append("<option selected value=""" + dt.Rows(i).Item("contentType").ToString + """>" + dt.Rows(i).Item("contentTypeName") + "</option>")
                Else
                    result.Append("<option value=""" + dt.Rows(i).Item("contentType").ToString + """>" + dt.Rows(i).Item("contentTypeName") + "</option>")
                End If
            Next
            result.Append("</select> ")
        End If

        Return result.ToString
    End Function

    Private Function bindSelImageSetting(ByVal value As String) As String
        Dim result As New StringBuilder
        Dim i As Integer
        Dim dt As New DataTable

        dt = getContentImgSettingLookup()
        If dt.Rows.Count > 0 Then
            result.Append("<select id=""selImageSetting"" name=""selImageSetting"" >")
            For i = 0 To dt.Rows.Count - 1
                If value = dt.Rows(i).Item("imgSetting").ToString Then
                    result.Append("<option selected value=""" + dt.Rows(i).Item("imgSetting").ToString + """>" + dt.Rows(i).Item("imgSettingName") + "</option>")
                Else
                    result.Append("<option value=""" + dt.Rows(i).Item("imgSetting").ToString + """>" + dt.Rows(i).Item("imgSettingName") + "</option>")
                End If
            Next
            result.Append("</select> ")
        End If

        Return result.ToString
    End Function

    Private Function bindSelImagePosition(ByVal value As String) As String
        Dim result As New StringBuilder
        Dim i As Integer
        Dim dt As New DataTable

        dt = getContentImgPositionLookup()
        If dt.Rows.Count > 0 Then
            result.Append("<select id=""selImagePosition"" name=""selImagePosition"" >")
            For i = 0 To dt.Rows.Count - 1
                If value = dt.Rows(i).Item("imgPosition").ToString Then
                    result.Append("<option selected value=""" + dt.Rows(i).Item("imgPosition").ToString + """>" + dt.Rows(i).Item("imgPositionName") + "</option>")
                Else
                    result.Append("<option value=""" + dt.Rows(i).Item("imgPosition").ToString + """>" + dt.Rows(i).Item("imgPositionName") + "</option>")
                End If
            Next
            result.Append("</select> ")
        End If

        Return result.ToString
    End Function

    Private Function bindThumbnail(ByVal tagRef As String, ByVal domainRef As String, ByVal contentRef As String) As String
        Dim result As New StringBuilder

        If Trim(contentRef) = "" Then
            result.Append("<div class=""fNotif mt5 mb5"">After save, you can insert the thumbnail.</div>")
        Else
            Dim dt As New DataTable
            Dim dtImg As New DataTable

            dtImg = getTagImageSetting(domainRef, getContentFirstTag(domainRef, contentRef))

            dt = getContentImage(domainRef, contentRef, "T")
            'If dt.Rows.Count > 0 Then
            '    For i = 0 To dt.Rows.Count - 1
            '        result.Append("<div class=""ov mb5"">")
            '        result.Append("  <img src=""" + rootPath + "wf/support/editForm.aspx?m=content&tr=" + tagRef + "&d=" + domainRef + "&ir=" + dt.Rows(i).Item("imgRef").ToString + "&w=0"" style=""width:100%"" />")
            '        result.Append("</div>")
            '        result.Append("<div class=""mb5"">")
            '        result.Append("  <a href=""javascript:doDeleteImage(" + dt.Rows(i).Item("imgRef").ToString + ");"">Delete Image</a>")
            '        result.Append("</div>")
            '    Next
            'Else
            '    'no image
            'End If

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    result.Append("<div class=""ov mb5"">")
                    result.Append("  <a href=""" + rootPath + "wf/support/editForm.aspx?src=contentTagPopup.aspx&m=contentPic&tr=" + tagRef + "&t=m&ir=0&im=" + dt.Rows(i).Item("imgRef").ToString + """);""> <img src=""" + rootPath + "wf/support/displayImage.aspx?m=content&d=" + domainRef + "&ir=" + dt.Rows(i).Item("imgRef").ToString + "&w=0"" style=""width:100%"" />")
                    result.Append("  </a>")
                    result.Append("</div>")
                    result.Append("<div class=""mb5"">")
                    result.Append("  <a href=""javascript:doDeleteImage(" + dt.Rows(i).Item("imgRef").ToString + ");"">Delete Image</a>")
                    result.Append("</div>")
                    result.Append("<div class=""mb5"">")
                    result.Append("  <a href=""" + rootPath + "wf/support/editForm.aspx?src=contentTagPopup.aspx&m=contentPic&tr=" + tagRef + "&t=m&ir=0&im=" + dt.Rows(i).Item("imgRef").ToString + """);"">Edit Image</a>")
                    result.Append("</div>")
                Next

            Else
                'no image
            End If

            'If tagRef = _parentTagRefNilaiNilaiPerusahaan Then
            '    If dt.Rows.Count = 0 Then
            '        result.Append("<div class=""fNotif mt5 mb5""><a href=""" + rootPath + "wf/support/uploadForm.aspx?src=contentTagPopup.aspx&m=contentThumb&tr=" + tagRef + "&t=s&ir=0&r=" + contentRef + """>Click here</a> to upload.</div>")
            '        result.Append("<div style=""font-size: 10px;font-weight: bold"">*Thumbnail must be size width: 400px and height: 300px</div>")
            '    End If
            '    If dtImg.Rows.Count > 0 Then
            '        If dtImg.Rows(0).Item("thumbImgW") <> 0 And dtImg.Rows(0).Item("thumbImgH") <> 0 Then
            '            result.Append("<div class=""fNotif mt5 mb5""><a href=""" + rootPath + "wf/support/uploadForm.aspx?src=contentTagPopup.aspx&m=contentThumb&tr=" + tagRef + "&t=s&ir=1&rw=" + dtImg.Rows(0).Item("thumbImgW").ToString + "&rh=" + dtImg.Rows(0).Item("thumbImgH").ToString + "&r=" + contentRef + """>Click here</a> to upload, auto resize to " + dtImg.Rows(0).Item("thumbImgW").ToString + "(w) x " + dtImg.Rows(0).Item("thumbImgH").ToString + "(h) if bigger than.</div>")
            '        End If
            '    End If
            'Else
            result.Append("<div class=""fNotif mt5 mb5""><a href=""" + rootPath + "wf/support/uploadForm.aspx?src=contentTagPopup.aspx&m=contentThumb&tr=" + tagRef + "&t=s&ir=0&r=" + contentRef + """>Click here</a> to upload.</div>")
            If tagRef = _contentTagRefBackgroundSlider Then
                result.Append("<div style=""font-size: 10px;font-weight: bold"">*Max size 500KB</div>")
            Else
                result.Append("<div style=""font-size: 10px;font-weight: bold"">*Max size 500KB</div>")
            End If

            If dtImg.Rows.Count > 0 Then
                If dtImg.Rows(0).Item("thumbImgW") <> 0 And dtImg.Rows(0).Item("thumbImgH") <> 0 Then
                    result.Append("<div class=""fNotif mt5 mb5""><a href=""" + rootPath + "wf/support/uploadForm.aspx?src=contentTagPopup.aspx&m=contentThumb&tr=" + tagRef + "&t=s&ir=1&rw=" + dtImg.Rows(0).Item("thumbImgW").ToString + "&rh=" + dtImg.Rows(0).Item("thumbImgH").ToString + "&r=" + contentRef + """>Click here</a> to upload, auto resize to " + dtImg.Rows(0).Item("thumbImgW").ToString + "(w) x " + dtImg.Rows(0).Item("thumbImgH").ToString + "(h) if bigger than.</div>")
                End If
            End If
            'End If

        End If

        Return result.ToString
    End Function

   Private Function bindPicture(ByVal tagRef As String, ByVal domainRef As String, ByVal contentRef As String) As String
        Dim result As New StringBuilder

        If Trim(contentRef) = "" Then
            result.Append("<div class=""alert alert-info fade in alert-dismissible"">After save, you can insert the picture.</div>")
        Else
            Dim dt As New DataTable
            Dim dt2 As New DataTable
            Dim dtImg As New DataTable

            dtImg = getTagImageSetting(domainRef, getContentFirstTag(domainRef, contentRef))

            dt = getContentImage(domainRef, contentRef, "T")
            dt2 = getContentImage(domainRef, contentRef, "P")

            dt.Merge(dt2)
            'dt.DefaultView.Sort = ""
            dt = dt.DefaultView.ToTable()

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    result.Append("<div class=""ov mb5"">")
                    result.Append("  <a href=""" + rootPath + "wf/support/editForm.aspx?src=contentTagPopup.aspx&m=contentPic&tr=" + tagRef + "&t=m&ir=0&im=" + dt.Rows(i).Item("imgRef").ToString + """);""> <img src=""" + rootPath + "wf/support/displayImage.aspx?m=content&d=" + domainRef + "&ir=" + dt.Rows(i).Item("imgRef").ToString + "&w=0"" style=""width:100%"" />")
                    result.Append("  </a>")
                    result.Append("</div>")
                    result.Append("<div class=""mb5"">")
                    result.Append("  <a href=""javascript:doDeleteImage(" + dt.Rows(i).Item("imgRef").ToString + ");"">Delete Image</a>")
                    result.Append("</div>")
                    result.Append("<div class=""mb5"">")
                    result.Append("  <a href=""" + rootPath + "wf/support/editForm.aspx?src=contentTagPopup.aspx&m=contentPic&tr=" + tagRef + "&t=m&ir=0&im=" + dt.Rows(i).Item("imgRef").ToString + """);"">Edit Image</a>")
                    result.Append("</div>")
                Next

            Else
                'no image
            End If

            'Dim dtImage As New DataTable
            ''dtImage = getIMG_TR_imagePic(domainRef, imgRef) '(q_domainRef, q_imageRef)
            'If dt.Rows.Count > 0 Then

            '    If Not IsDBNull(dt.Rows(0).Item("imgRef")) Then
            '        dtImage = getIMG_TR_imagePic(domainRef, imgRef) '(q_domainRef, q_imageRef)

            '        ImgBytes = dtImage.Rows(0).Item("imgFile")
            '        _fileImgByte = Convert.ToBase64String(ImgBytes)
            '        _fileImgName = dtImage.Rows(0).Item("imgFileName")
            '        '_fileImgUrl = SaveDocumentImageToFolder(q_vendorRef, _letterCategorySIUP, dtPicture.Rows(0).Item("fileName"), SIUPFileExt, SIUPBytes, "SIUP")
            '        _fileImgUrl = SaveDocumentImageToFolder(domainRef, contentRef, dtImage.Rows(0).Item("imgFileName"), ImgBytes, "IMAGE")
            '    End If
            'End If

            'If dt.Rows.Count > 0 Then
            '    For i = 0 To dt.Rows.Count - 1
            '        result.Append("<div class=""ov mb5"">")
            '        'result.Append("  <img src=""" + rootPath + "wf/support/displayImage.aspx?m=content&d=" + domainRef + "&ir=" + dt.Rows(i).Item("imgRef").ToString + "&w=0"" style=""width:100%"" />")
            '        result.Append("<input type=""file"" id=""filename"" name=""filename"" class=""file"" data-show-upload=""false"" onchange=""javascript:readFile('IMAGE',this);"" data-preview-file-type=""text""  />")
            '        result.Append("</div>")
            '        result.Append("<div class=""mb5"">")
            '        result.Append("  <a href=""javascript:doDeleteImage(" + dt.Rows(i).Item("imgRef").ToString + ");"">Delete Image</a>")
            '        result.Append("</div>")
            '    Next

            'Else
            '    'no image
            'End If
            If tagRef = _contentTagRefBackgroundSlider Then
                result.Append("<div style=""font-size: 10px;font-weight: bold"">*Background slider must be size width: 1583px and height: 500px</div>")
            End If
            'result.Append("<div class=""fNotif mt5 mb5""><a href=""" + rootPath + "wf/support/uploadForm.aspx?src=contentTagPopup.aspx&m=contentPic&tr=" + tagRef + "&t=m&ir=0&r=" + contentRef + """>Click here</a> to upload Cropper.</div>")
            result.Append("<div class=""fNotif mt5 mb5""><a href=""" + rootPath + "wf/support/uploadFormStandard.aspx?src=contentTagPopup.aspx&m=contentThumb&tr=" + tagRef + "&t=s&ir=0&r=" + contentRef + """>Click here</a> to upload Standard.</div>")
            If tagRef = _contentTagRefBackgroundSlider Then
                result.Append("<div style=""font-size: 10px;font-weight: bold"">*Max size 500KB</div>")
            Else
                result.Append("<div style=""font-size: 10px;font-weight: bold"">*Max size 250KB</div>")
            End If

            If dtImg.Rows.Count > 0 Then
                If dtImg.Rows(0).Item("picImgW") <> 0 And dtImg.Rows(0).Item("picImgH") <> 0 Then
                    result.Append("<div class=""fNotif mt5 mb5""><a href=""" + rootPath + "wf/support/uploadForm.aspx?src=contentTagPopup.aspx&m=contentPic&tr=" + tagRef + "&t=m&ir=1&rw=" + dtImg.Rows(0).Item("picImgW").ToString + "&rh=" + dtImg.Rows(0).Item("picImgH").ToString + "&r=" + contentRef + """>Click here</a> to upload, auto resize to " + dtImg.Rows(0).Item("picImgW").ToString + "(w) x " + dtImg.Rows(0).Item("picImgH").ToString + "(h) if bigger than.</div>")
                End If
            End If
        End If

        Return result.ToString
    End Function

    Private Function bindAttachment(ByVal tagRef As String, ByVal domainRef As String, ByVal contentRef As String) As String
        Dim result As New StringBuilder

        If Trim(contentRef) = "" Then
            result.Append("<div class=""fNotif mt5 mb5"">After save, you can upload the attachment.</div>")
        Else
            Dim dt As New DataTable


            dt = getContentAttachmentRef(domainRef, contentRef)

            If dt.Rows.Count > 0 Then

                For i = 0 To dt.Rows.Count - 1
                    result.Append("<div class=""ov mb5"">")
                    Select Case Right(dt.Rows(i).Item("attachFN"), 4).ToLower
                        Case ".pdf"
                            result.Append("<a target=""_blank"" href=""" + rootPath + "wf/support/displayAttachment.aspx?dr=" + domainRef + "&cr=" + contentRef + "&ar=" + dt.Rows(i).Item("attachRef").ToString + """><img border=""0"" src=""" + rootPath + "support/image/icon_pdf.jpg"" /></a>")
                        Case ".doc"
                            result.Append("<a target=""_blank"" href=""" + rootPath + "wf/support/displayAttachment.aspx?dr=" + domainRef + "&cr=" + contentRef + "&ar=" + dt.Rows(i).Item("attachRef").ToString + """><img border=""0"" src=""" + rootPath + "support/image/icon_word.jpg"" /></a>")
                        Case "docx"
                            result.Append("<a target=""_blank"" href=""" + rootPath + "wf/support/displayAttachment.aspx?dr=" + domainRef + "&cr=" + contentRef + "&ar=" + dt.Rows(i).Item("attachRef").ToString + """><img border=""0"" src=""" + rootPath + "support/image/icon_word.jpg"" /></a>")
                        Case ".xls"
                            result.Append("<a target=""_blank"" href=""" + rootPath + "wf/support/displayAttachment.aspx?dr=" + domainRef + "&cr=" + contentRef + "&ar=" + dt.Rows(i).Item("attachRef").ToString + """><img border=""0"" src=""" + rootPath + "support/image/icon_excel.jpg"" /></a>")
                        Case "xlsx"
                            result.Append("<a target=""_blank"" href=""" + rootPath + "wf/support/displayAttachment.aspx?dr=" + domainRef + "&cr=" + contentRef + "&ar=" + dt.Rows(i).Item("attachRef").ToString + """><img border=""0"" src=""" + rootPath + "support/image/icon_excel.jpg"" /></a>")
                        Case Else
                            result.Append("<a target=""_blank"" href=""" + rootPath + "wf/support/displayAttachment.aspx?dr=" + domainRef + "&cr=" + contentRef + "&ar=" + dt.Rows(i).Item("attachRef").ToString + """><img border=""0"" src=""" + rootPath + "support/image/icon_file.png"" /></a>")
                    End Select
                    result.Append("</div>")
                    result.Append("<div class=""mb5"">")
                    result.Append("  <a href=""javascript:doDeleteAttachment(" + dt.Rows(i).Item("attachRef").ToString + ");"">Delete Attachment</a>")
                    result.Append("</div>")
                Next

            Else
                'no attachment
            End If
            'result.Append("<div class=""fNotif mt5 mb5""><a href=""" + rootPath + "wf/support/uploadForm.aspx?src=contentTagPopup.aspx&m=contentAttach&tr=" + tagRef + "&t=m&r=" + contentRef + """>Click here</a> to upload.</div>")
            result.Append("<div class=""fNotif mt5 mb5""><a href=""" + rootPath + "wf/support/uploadFormStandard.aspx?src=contentTagPopup.aspx&m=contentAttach&tr=" + tagRef + "&t=m&r=" + contentRef + """>Click here</a> to upload.</div>")
            result.Append("<div style=""font-size: 10px;font-weight: bold"">*Max size 20MB</div>")

        End If

        Return result.ToString
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim inputUN As String = Session("userName")
        If inputUN = String.Empty Then inputUN = ""

        Dim q_ref As String = Request.QueryString("r")
        Dim q_tagRef As String = Request.QueryString("tr")

        tagRef = q_tagRef

        If IsPostBack Then
            isPB = "1"

            Dim _save As String = Request.Form("_save")
            Dim _saveClose As String = Request.Form("_saveClose")
            Dim _delete As String = Request.Form("_delete")
            Dim _deleteImage As String = Request.Form("_deleteImage")
            Dim _deleteAttachment As String = Request.Form("_deleteAttachment")
            Dim _removeImage As String = Request.Form("_removeImage")



            If Trim(_save) = "1" Or Trim(_saveClose) = "1" Then
                selContentType = Request.Form("selContentType")
                selImageSetting = Request.Form("selImageSetting")
               
                selImagePosition = Request.Form("selImagePosition")
                txtMetaTitle = Request.Form("txtMetaTitle")
                txtMetaAuthor = Request.Form("txtMetaAuthor")
                txtKeyword = Request.Form("txtKeyword")
                txtMetaDescription = Request.Form("txtMetaDescription")
                txtVideo = Request.Form("txtVideo")
                txtTitle = Request.Form("txtTitle")
                txtTitleDetail = Request.Form("txtTitleDetail")

                txtMapLongitude = Request.Form("txtMapLongitude")
                txtMapLatitude = Request.Form("txtMapLatitude")


                txtSynopsis = Request.Form("txtSynopsis")
                txtSynopsisNNP = Request.Form("txtSynopsisNNP")
                txtContent = Request.Form("txtContent")


                selDayContent = Request.Form("selDayContent")
                selMonthContent = Request.Form("selMonthContent")
                txtYearContent = Request.Form("txtYearContent")

                selDayPublish = Request.Form("selDayPublish")
                selMonthPublish = Request.Form("selMonthPublish")
                txtYearPublish = Request.Form("txtYearPublish")

                selDayExpired = Request.Form("selDayExpired")
                selMonthExpired = Request.Form("selMonthExpired")
                txtYearExpired = Request.Form("txtYearExpired")

                Dim contentDate As System.Data.SqlTypes.SqlDateTime
                contentDate = System.Data.SqlTypes.SqlDateTime.Null
                If selDayContent <> "-" And selMonthContent <> "-" And txtYearContent <> "" Then
                    contentDate = New System.Data.SqlTypes.SqlDateTime(txtYearContent, selMonthContent, selDayContent)
                End If

                Dim publishDate As System.Data.SqlTypes.SqlDateTime
                publishDate = System.Data.SqlTypes.SqlDateTime.Null
                If selDayPublish <> "-" And selMonthPublish <> "-" And txtYearPublish <> "" Then
                    publishDate = New System.Data.SqlTypes.SqlDateTime(txtYearPublish, selMonthPublish, selDayPublish)
                ElseIf cekTagIsPublish(Session("domainRef").ToString, q_tagRef) = "0" Then
                    publishDate = New System.Data.SqlTypes.SqlDateTime(Now.Year, Now.Month, Now.Day)
                End If

                Dim expiredDate As System.Data.SqlTypes.SqlDateTime
                expiredDate = System.Data.SqlTypes.SqlDateTime.Null
                If selDayExpired <> "-" And selMonthExpired <> "-" And txtYearExpired <> "" Then
                    expiredDate = New System.Data.SqlTypes.SqlDateTime(txtYearExpired, selMonthExpired, selDayExpired)
                End If

                'ini musti cek approval belum nih
                Dim approvedDate As System.Data.SqlTypes.SqlDateTime
                approvedDate = New System.Data.SqlTypes.SqlDateTime(Now)


                Dim temp As String = ""
                Dim Hashtable As New Hashtable

                If txtSynopsis <> "" Then synopsis = txtSynopsis
                'If txtSynopsisNNP <> "" Then synopsis = txtSynopsisNNP

                If Trim(q_ref) <> "" Then
                    'update
                    temp = updateContent(Session("domainRef").ToString, q_ref, selContentType, selImageSetting, selImagePosition, txtVideo, txtTitle, txtTitleDetail, _
                                         txtMapLatitude, txtMapLongitude, synopsis, txtContent, _
                                         contentDate, publishDate, expiredDate, approvedDate, txtMetaDescription, txtMetaTitle, txtMetaAuthor)
                Else
                    'insert
                    temp = insertContent(Session("domainRef").ToString, selContentType, selImageSetting, selImagePosition, txtVideo, txtTitle, txtTitleDetail, _
                                         txtMapLatitude, txtMapLongitude, synopsis, txtContent, _
                                         contentDate, publishDate, expiredDate, approvedDate, txtMetaDescription, inputUN, txtMetaTitle, txtMetaAuthor)
                End If

                If IsNumeric(temp) Then
                    insertContentKeyword(Session("domainRef").ToString, temp, txtKeyword, _keywordSplitter)
                    insertContentTag(Session("domainRef").ToString, temp, q_tagRef)

                    If Trim(_save) = "1" Then
                        If Trim(q_ref) <> "" Then
                            'update
                            Hashtable("note") = "Update succeed."
                        Else
                            'insert
                            Hashtable("note") = "Insert succeed."
                        End If

                        Response.Redirect(GetEncUrl(_rootPath + "wf/contentTagPopup.aspx?tr=" + q_tagRef + "&r=" + temp + "&", Hashtable))

                    Else
                        If Trim(q_ref) <> "" Then
                            'update
                            Page.ClientScript.RegisterStartupScript(Me.GetType, "closePopup", "<script language='javascript'>try{window.opener.doRefresh();}catch(e){}; window.close();</script>")
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType, "closePopup", "<script language='javascript'>try{window.opener.doRefresh();}catch(e){}; alert('Insert succeed, please click Refresh / Search button'); window.close();</script>")
                        End If
                    End If

                Else
                    Hashtable("note") = "Sorry, there is an error.<br>" + _
                                "Error Message: " + temp + "<br><br>" + _
                                "<a href=""javascript:window.close();"">Click here</a> to close this popup.<br>" + _
                                "Thankyou."

                    Response.Redirect(GetEncUrl(_rootPath + "wf/notificationpopup.aspx?", Hashtable))

                End If
            End If

            If Trim(_delete) = "1" Then
                Dim temp As String = ""
                Dim Hashtable As New Hashtable


                temp = deleteContent(Session("domainRef").ToString, q_ref)
                If Trim(temp) = "" Then
                    'benar
                    Page.ClientScript.RegisterStartupScript(Me.GetType, "closePopup", "<script language='javascript'>try{window.opener.doRefresh();}catch(e){}; alert('Delete succeed, please click Refresh / Search button'); window.close();</script>")
                Else
                    'salah
                    Hashtable("note") = "Sorry, there is an error.<br>" + _
                                "Error Message: " + temp + "<br><br>" + _
                                "<a href=""javascript:window.close();"">Click here</a> to close this popup.<br>" + _
                                "Thankyou."

                    Response.Redirect(GetEncUrl(_rootPath + "wf/notificationpopup.aspx?", Hashtable))

                End If
            End If

            If Trim(_deleteImage) <> "" Then
                Dim temp As String = ""
                Dim Hashtable As New Hashtable


                temp = deleteImage(Session("domainRef").ToString, _deleteImage)
                If Trim(temp) = "" Then
                    'benar
                    Hashtable("note") = "Delete image succeed."

                    Response.Redirect(GetEncUrl(_rootPath + "wf/contentTagPopup.aspx?tr=" + q_tagRef + "&r=" + q_ref + "&", Hashtable))

                Else
                    'salah
                    Hashtable("note") = "Sorry, there is an error.<br>" + _
                                "Error Message: " + temp + "<br><br>" + _
                                "<a href=""javascript:window.close();"">Click here</a> to close this popup.<br>" + _
                                "Thankyou."

                    Response.Redirect(GetEncUrl(_rootPath + "wf/notificationpopup.aspx?", Hashtable))

                End If
            End If

            If Trim(_removeImage) <> "" Then
                Dim temp As String = ""
                Dim Hashtable As New Hashtable


                temp = removeImage(Session("domainRef").ToString, q_ref, _removeImage)
                If Trim(temp) = "" Then
                    'benar
                    Hashtable("note") = "Remove image from this content succeed."

                    Response.Redirect(GetEncUrl(_rootPath + "wf/contentTagPopup.aspx?tr=" + q_tagRef + "&r=" + q_ref + "&", Hashtable))

                Else
                    'salah
                    Hashtable("note") = "Sorry, there is an error.<br>" + _
                                "Error Message: " + temp + "<br><br>" + _
                                "<a href=""javascript:window.close();"">Click here</a> to close this popup.<br>" + _
                                "Thankyou."

                    Response.Redirect(GetEncUrl(_rootPath + "wf/notificationpopup.aspx?", Hashtable))

                End If
            End If

            If Trim(_deleteAttachment) <> "" Then
                Dim temp As String = ""
                Dim Hashtable As New Hashtable


                temp = deleteAttachment(Session("domainRef").ToString, q_ref, _deleteAttachment, q_tagRef)
                If Trim(temp) = "" Then
                    'benar
                    Hashtable("note") = "Delete attachment succeed."

                    Response.Redirect(GetEncUrl(_rootPath + "wf/contentTagPopup.aspx?tr=" + q_tagRef + "&r=" + q_ref + "&", Hashtable))

                Else
                    'salah
                    Hashtable("note") = "Sorry, there is an error.<br>" + _
                                "Error Message: " + temp + "<br><br>" + _
                                "<a href=""javascript:window.close();"">Click here</a> to close this popup.<br>" + _
                                "Thankyou."

                    Response.Redirect(GetEncUrl(_rootPath + "wf/notificationpopup.aspx?", Hashtable))

                End If
            End If
        Else
            ''''' not postback '''''
            ''''' not postback '''''
            ''''' not postback '''''

            If Not Request.Params("x") Is Nothing Then
                Dim param As Hashtable = GetDecParam(Request.Params("x"))
                Dim q_note As String = param("note")

                selContentType = param("contentType")
                selImageSetting = param("imageSetting")
                selImagePosition = param("ip")
                selDayContent = param("dc")
                selMonthContent = param("mc")
                txtYearContent = param("yc")
                selDayPublish = param("dp")
                selMonthPublish = param("mp")
                txtYearPublish = param("yp")
                selDayExpired = param("de")
                selMonthExpired = param("me")
                txtYearExpired = param("ye")

                If Trim(q_note) <> "" Then
                    divNotif.InnerHtml = "Notification: " + q_note
                Else
                    divNotif.InnerHtml = "Notification: Please fill all data below than click ""save"""
                End If
            End If

            If Trim(q_ref) <> "" Then
                isUpdate = "1"

                CType(Master.FindControl("titlePopup"), Web.UI.HtmlControls.HtmlGenericControl).InnerHtml = getTagTypeNameByTagRef(Session("domainRef").ToString, q_tagRef) + " :: " + getTagParentNameRekursif(Session("domainRef").ToString, q_tagRef) + " :: Update Content [" + Session("domain") + "]"
                Page.Title = "CMS :: " + Session("domain") + " :: Update Content"

                Dim dt As New DataTable

                dt = getContentInfo(Session("domainRef").ToString, q_ref)

                'dari sql
                If dt.Rows.Count > 0 Then
                    selContentType = dt.Rows(0).Item("contentType")
                    selImageSetting = dt.Rows(0).Item("imgSetting")
                    selImagePosition = dt.Rows(0).Item("imgPosition")
                    txtMetaTitle = dt.Rows(0).Item("metaTitle")
                    txtMetaAuthor = dt.Rows(0).Item("metaAuthor")
                    txtKeyword = getContentKeywordStr(Session("domainRef").ToString, q_ref)
                    txtMetaDescription = dt.Rows(0).Item("metaDescription")
                    txtVideo = dt.Rows(0).Item("embedVideo")
                    txtTitle = dt.Rows(0).Item("title")
                    txtTitleDetail = dt.Rows(0).Item("titleDetail")

                    If Not IsDBNull(dt.Rows(0).Item("Latitude")) Then
                        txtMapLatitude = dt.Rows(0).Item("Latitude")
                    End If

                    If Not IsDBNull(dt.Rows(0).Item("Longitude")) Then
                        txtMapLongitude = dt.Rows(0).Item("Longitude")
                    End If

                    txtSynopsisNNP = dt.Rows(0).Item("synopsis")
                    txtSynopsis = dt.Rows(0).Item("synopsis")

                    txtContent = dt.Rows(0).Item("content")

                    If Not IsDBNull(dt.Rows(0).Item("contentDate")) Then
                        selDayContent = Day(dt.Rows(0).Item("contentDate"))
                        selMonthContent = Month(dt.Rows(0).Item("contentDate"))
                        txtYearContent = Year(dt.Rows(0).Item("contentDate"))
                    End If

                    If Not IsDBNull(dt.Rows(0).Item("publishDate")) Then
                        selDayPublish = Day(dt.Rows(0).Item("publishDate"))
                        selMonthPublish = Month(dt.Rows(0).Item("publishDate"))
                        txtYearPublish = Year(dt.Rows(0).Item("publishDate"))
                    End If

                    If Not IsDBNull(dt.Rows(0).Item("expiredDate")) Then
                        selDayExpired = Day(dt.Rows(0).Item("expiredDate"))
                        selMonthExpired = Month(dt.Rows(0).Item("expiredDate"))
                        txtYearExpired = Year(dt.Rows(0).Item("expiredDate"))
                    End If

                    '' mungkin perlu param tag list


                    'divHitView.InnerHtml = dt.Rows(0).Item("hit").ToString

                    Dim Hashtable As New Hashtable
                    Hashtable("contentType") = dt.Rows(0).Item("contentType")
                    Hashtable("imageSetting") = dt.Rows(0).Item("imgSetting")
                    Hashtable("dc") = selDayContent
                    Hashtable("mc") = selMonthContent
                    Hashtable("yc") = txtYearContent
                    Hashtable("dp") = selDayPublish
                    Hashtable("mp") = selMonthPublish
                    Hashtable("yp") = txtYearPublish
                    Hashtable("de") = selDayExpired
                    Hashtable("me") = selMonthExpired
                    Hashtable("ye") = txtYearExpired
                    Hashtable("isForm") = 1

                    ltrBtn.Text = "<div class=""linkBtn left mr5""> " + _
                                    "  <a href=""javascript:doDelete('" + MyURLEncode(dt.Rows(0).Item("title")) + "');"">Delete</a> " + _
                                    "</div> " + _
                                    "<div class=""linkBtn left mr5""> " + _
                                    "  <a href=""" + GetEncUrl(_rootPath + "wf/contentTagPopup.aspx?tr=" + q_tagRef + "&", Hashtable) + """>Insert New</a> " + _
                                    "</div> "
                    'ltrBtnTop.Text = "<div class=""linkBtn left mr5""> " + _
                    '                "  <a href=""javascript:doDelete('" + MyURLEncode(dt.Rows(0).Item("title")) + "');"">Delete</a> " + _
                    '                "</div> " + _
                    '                "<div class=""linkBtn left mr5""> " + _
                    '                "  <a href=""" + GetEncUrl(_rootPath + "wf/contentTagPopup.aspx?tr=" + q_tagRef + "&", Hashtable) + """>Insert New</a> " + _
                    '                "</div> "
                End If

            Else



                CType(Master.FindControl("titlePopup"), Web.UI.HtmlControls.HtmlGenericControl).InnerHtml = getTagTypeNameByTagRef(Session("domainRef").ToString, q_tagRef) + " :: " + getTagParentNameRekursif(Session("domainRef").ToString, q_tagRef) + " :: Insert Content [" + Session("domain") + "]"

                Page.Title = "CMS :: " + Session("domain") + " :: Insert Content"
            End If



            ''''' set form permission '''''
            ''''' set form permission '''''
            ''''' set form permission '''''
            If Trim(q_tagRef) <> "" Then
                Dim dtTag As New DataTable

                dtTag = getTagPermissionList(Session("domainRef").ToString, q_tagRef)
                If dtTag.Rows.Count > 0 Then
                    For i = 0 To dtTag.Rows.Count - 1
                        If dtTag.Rows(i).Item("isTitle") = "1" Then isTitle = "1"
                        If dtTag.Rows(i).Item("isTitleDetail") = "1" Then isTitleDetail = "1"
                        If dtTag.Rows(i).Item("isMap") = "1" Then isMap = "1"
                        If dtTag.Rows(i).Item("isSynopsis") = "1" Then isSynopsis = "1"
                        If dtTag.Rows(i).Item("isSynopsis") = "1" Then isSynopsisNNP = "1"
                        If dtTag.Rows(i).Item("isContent") = "1" Then isContent = "1"
                        If dtTag.Rows(i).Item("isThumbnail") = "1" Then isThumbnail = "1"
                        If dtTag.Rows(i).Item("isPicture") = "1" Then isPicture = "1"
                        If dtTag.Rows(i).Item("isVideo") = "1" Then isVideo = "1"
                        If dtTag.Rows(i).Item("isAttachment") = "1" Then isAttachment = "1"
                        If dtTag.Rows(i).Item("isContentDate") = "1" Then isDate = "1"
                        If dtTag.Rows(i).Item("isContentPubDate") = "1" Then isPublishDate = "1"
                        If dtTag.Rows(i).Item("isExpiredDate") = "1" Then isExpiredDate = "1"
                    Next
                End If

                If Trim(q_ref) = "" Then
                    If isDate <> "1" Then
                        selDayContent = "-"
                        selMonthContent = "-"
                        txtYearContent = ""
                    Else
                        If Trim(selDayContent) = "-" Or Trim(selDayContent) = "" Then selDayContent = Day(Now)
                        If Trim(selMonthContent) = "-" Or Trim(selMonthContent) = "" Then selMonthContent = Month(Now)
                        If Trim(txtYearContent) = "" Then txtYearContent = Year(Now)
                    End If

                    If isPublishDate <> "1" Then
                        selDayPublish = "-"
                        selMonthPublish = "-"
                        txtYearPublish = ""
                    Else
                        If Trim(selDayPublish) = "-" Or Trim(selDayPublish) = "" Then selDayPublish = Day(Now)
                        If Trim(selMonthPublish) = "-" Or Trim(selMonthPublish) = "" Then selMonthPublish = Month(Now)
                        If Trim(txtYearPublish) = "" Then txtYearPublish = Year(Now)
                    End If
                End If
            End If
            ''''' set form permission end '''''
            ''''' set form permission end '''''
            ''''' set form permission end '''''

            ltrContentType.Text = bindSelContentType(selContentType)
            ltrImageSetting.Text = bindSelImageSetting(selImageSetting)
            ltrImagePosition.Text = bindSelImagePosition(selImagePosition)

            ltrThumbnail.Text = bindThumbnail(q_tagRef, Session("domainRef").ToString, q_ref)
            ltrPicture.Text = bindPicture(q_tagRef, Session("domainRef").ToString, q_ref)
            ltrAttachment.Text = bindAttachment(q_tagRef, Session("domainRef").ToString, q_ref)
        End If
    End Sub


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        cekIsNotLoginWebPopup()
    End Sub
End Class