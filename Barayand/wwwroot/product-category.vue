<template>
    <div style="width:85%;margin:auto;padding-top:10px;color:#7b7c87">
        
        <v-layout row wrap>
            <v-flex xs12 md12 lg12 xl12 style="padding:15px;padding-bottom:2px !important"><!--Row #1-->
                <v-layout row wrap>
                    <v-flex xs12 sm12 md10 lg10 xl10 style="padding:15px;padding-bottom:0px;">
                    <div style="padding-top:23px;font-weight:bolder;font-size:20px">مدیریت دسته بندی محصولات</div>
                    </v-flex>
                </v-layout>
            </v-flex>
            <v-flex xs12 md12 lg12 xl12 style="padding:15px;"><!--Row #2-->
                <div class="operationBoxDesign">
                    <v-alert width="100%" type="warning" color="rgb(38, 45, 71)" outlined>
                        <v-progress-linear rounded color="orange" striped height="10" :value="RegisteredLevelsPercentage+'%'">
                        </v-progress-linear>
                        
                        <div style="padding:10px;padding-top:25px !important">
                            کابر گرامی ؛ حداکثر تعداد سطوح مجاز {{MaxLevelCount}} سطح میباشد که تاکنون {{MaxUsedLevelCount}} سطح تعریف شده است. <br/>
                        </div>

                    </v-alert>
                </div>
                <v-card>
                    <v-card-title>
                        <v-layout row wrap>
                            <v-flex xs12 sm12 md7 lg7 xl7 style="padding-right:15px;padding-left:15px;">
                                <v-text-field outlined dense color="#262d47" placeholder="جستجو" v-model="searchcategory" append-icon="fas fa-search"></v-text-field>   
                            </v-flex>
                            <v-flex xs12 sm12 md5 lg5 xl5 style="padding-right:15px;padding-left:5px;">
                                <v-btn outlined color="#4CAF50" @click="CatCreationDialog = true" dark><v-icon small style="padding-left:5px;">fa fa-plus</v-icon>&nbsp;دسته بندی جدید</v-btn>&nbsp;
                                <JsonExcel style="display: inline-block;padding-left: 5px;" :data="originalItems" :name="(Math.floor(Math.random() * (1254152541 - 1111 + 1)) + 1111).toString()" :fields="ExportDataColumns">
                                  <v-btn outlined color="#2a2d45" dark><v-icon small style="padding-left:5px;">fa fa-file-export</v-icon>&nbsp;دریافت اکسل</v-btn>&nbsp;
                                </JsonExcel>
                                <v-btn outlined color="#AD1457" @click="CategoryTreeView" dark><v-icon small style="padding-left:5px;">fa fa-stream</v-icon>&nbsp;نمایش درختی</v-btn>
                            </v-flex>
                            <v-flex xs12 sm12 md12 lg12 xl12 style="padding-right:15px;padding-left:55px;">
                                <v-expansion-panels flat>
                                    <v-expansion-panel>
                                        <v-expansion-panel-header v-slot="{ open }"  disable-icon-rotate expand-icon="fa fa-filter">
                                            <v-row no-gutters>
                                            <v-col cols="4">فیلتر</v-col>
                                            <v-col cols="8">
                                                <v-fade-transition leave-absolute>
                                                    <span v-if="open">فیلتر بر اساس سطح دسته بندی</span>
                                                    <v-row v-else no-gutters style="width: 100%">
                                                        <v-col cols="12">سطح انتخاب شده : {{ filterlevel || '---' }}</v-col>
                                                    </v-row>
                                                </v-fade-transition>
                                            </v-col>
                                            </v-row>
                                        </v-expansion-panel-header>
                                        <v-expansion-panel-content>
                                            <v-row>
                                                <v-col cols="3" style="border-left:1px solid rgb(200,200,200)">
                                                    <v-row>
                                                        <v-col cols="12" style="font-size:13px;font-weight:bolder;">سطح :</v-col>
                                                        <v-col cols="12">
                                                            <v-autocomplete @change="filterCats" item-text="text" item-value="value" :items="filterLevelList" no-data-text="سطحی یافت نشد" v-model="filterlevel" color="#2a2d45" outlined dense label="سطح"></v-autocomplete>
                                                        </v-col>
                                                        <v-col cols="12" v-if="filterlevel > 0">
                                                            <v-btn outlined color="#AD1457" dark @click="clearFilter"><v-icon small style="padding-left:5px;">fa fa-broom</v-icon>&nbsp;حذف فیلتر</v-btn>
                                                        </v-col>
                                                    </v-row>
                                                </v-col>
                                                <v-col cols="3" style="border-left:1px solid rgb(200,200,200)">
                                                    <v-row>
                                                        <v-col cols="12" style="font-size:13px;font-weight:bolder;">وضعیت :</v-col>
                                                        <v-col cols="12" style="display: flex;align-items: center;justify-content: center;">
                                                            <v-switch @change="filterCats($event,'switch')" color="#2a2d45"></v-switch>
                                                        </v-col>
                                                        <v-col cols="12" v-if="flilteredStatus">
                                                            <v-btn outlined color="#AD1457" dark @click="clearFilter"><v-icon small style="padding-left:5px;">fa fa-broom</v-icon>&nbsp;حذف فیلتر</v-btn>
                                                        </v-col>
                                                    </v-row>
                                                </v-col>
                                            </v-row>
                                        </v-expansion-panel-content>
                                    </v-expansion-panel>
                                </v-expansion-panels>
                            </v-flex>
                        </v-layout>
                    </v-card-title>
                    <v-card-text>
                        <v-data-table no-data-text="رکوردی یافت نشد" no-results-text="رکوردی یافت نشد" :footer-props="{'items-per-page-text':'آیتم در هر صفحه :','items-per-page-all-text':'همه'}" group-by="pC_ParentTitle"  items-per-page-text="تعداد در هر صفحه" dense :loading="isLoadingCats" :headers="CatTableHeader" :search="searchcategory" :items="CatTableItems">
                            <template v-slot:[`item.pC_Status`]="{ item }">
                               <v-btn small text color="#D32F2F" @click="ActiveItem(item.pC_Id)" dark v-if="!item.pC_Status">غیر فعال</v-btn>
                               <v-btn small text color="#2E7D32" @click="DisableItem(item.pC_Id)" dark v-else> فعال</v-btn>
                            </template>
                            <template v-slot:[`item.pC_ParentTitle`]="{ item }">
                                <span class="categoryStyle">{{item.pC_ParentTitle}}</span>
                            </template>
                            <template v-slot:[`item.pC_Logo`]="{ item }">
                                <v-btn target="_blank" :href="LogoUrl+`${item.pC_Logo}`" icon small><v-icon small>fas fa-eye</v-icon></v-btn>
                            </template>
                            <template v-slot:[`item.operation`]="{ item }">
                                <v-tooltip top color="orange accent-4">
                                    <template v-slot:activator="{ on, attrs }">
                                        <v-btn v-bind="attrs" @click="AttributeDialogShow(item.pC_Id)" v-on="on" text icon small color="orange accent-4" v-if="item.pC_AttrAble"><v-icon small>fas fa-layer-group</v-icon></v-btn>&nbsp;
                                    </template>
                                    <span>فیلد های اختصاصی</span>
                                </v-tooltip>
                                <v-tooltip color="purple accent-4" top v-if="item.pC_ParentTitle == '****'">
                                    <template v-slot:activator="{ on, attrs }">
                                        <v-btn v-bind="attrs" @click="DsicountedBoxSetting(item.pC_Id)" v-on="on" text icon small color="purple accent-4"><v-icon small>fas fa-tags</v-icon></v-btn>&nbsp;
                                    </template>
                                    <span>باکس های تخفیفی</span>
                                </v-tooltip>
                                <v-tooltip color="green darken-4" top v-if="item.pC_ParentTitle == '****'">
                                    <template v-slot:activator="{ on, attrs }">
                                        <v-btn v-bind="attrs" @click="StaticAds(item.pC_Id)" v-on="on" text icon small color="green darken-4"><v-icon small>fas fa-ad</v-icon></v-btn>&nbsp;
                                    </template>
                                    <span>تبلیغات ثابت</span>
                                </v-tooltip>
                                <v-tooltip color="brown darken-4" top v-if="item.pC_ParentTitle == '****'">
                                    <template v-slot:activator="{ on, attrs }">
                                        <v-btn v-bind="attrs" @click="MoveableAds(item.pC_Id)" v-on="on" text icon small color="browb darken-4"><v-icon small>fab fa-adversal</v-icon></v-btn>&nbsp;
                                    </template>
                                    <span>تبلیغات متحرک</span>
                                </v-tooltip>
                                
                                <v-btn text icon small color="blue accent-4" @click="editItem(item)"><v-icon small>fas fa-edit</v-icon></v-btn>
                                <v-btn text icon small color="red accent-4" @click="confirmDeleteItem(item.pC_Id)"><v-icon small>fas fa-trash</v-icon></v-btn>&nbsp;
                            </template>

                        </v-data-table>
                    </v-card-text>
                </v-card>
            </v-flex>
        </v-layout>
        <template><!--Add New Cat From Dialog-->
            <v-dialog fullscreen persistent v-model="CatCreationDialog">
                <v-card>
                  <v-card-title style="padding:0px !important">
                      <v-app-bar dense color="rgb(42, 45, 69)" dark>
                          <v-toolbar-title style="color:orange">
                              <span v-if="!inEditingItem">ثبت دسته بندی جدید</span>
                              <span v-else>ویرایش دسته بندی  &nbsp; <v-btn small outlined color="red" @click="cancelEditItem"><v-icon small style="padding-left:5px;">fa fa-times</v-icon>&nbsp;لغو ویرایش</v-btn></span>
                          </v-toolbar-title>
                          <v-spacer></v-spacer>
                          <v-icon color="orange" @click="CatCreationDialog = false" small>fa fa-times</v-icon>
                      </v-app-bar>
                  </v-card-title>
                  <v-card-text>
                      <v-layout row wrap>
                          <v-flex xs12 md12 lg12 xl12 style="padding:15px;"><!--Row #2-->
                            <v-layout row wrap style="flex-wrap:wrap-reverse !important;">
                                <v-flex xs12 sm12 md4 lg4 xl4 style="padding:15px;padding-top:0px !important;border-left:1px solid rgb(200,200,200)"><!--Register New Cat From-->
                                    <div style="padding-top:23px;padding-bottom:5px;font-weight:bolder;font-size:17px">تنظیمات اصلی</div>
                                   
                                    <v-autocomplete :items="originalItems" item-text="pC_Title" item-value="pC_Id" no-data-text="دسته بندی یافت نشد" v-model="ParentCat" color="#2a2d45" outlined dense label="دسته بندی اصلی"></v-autocomplete>
                                   
                                    <v-text-field color="#2a2d45" outlined dense v-model="CatTitle" label="عنوان دسته بندی" ></v-text-field>
                                    
                                    <v-text-field color="#2a2d45" outlined dense v-model="CatImgTitle" label="عنوان زیر عکس" ></v-text-field>
                                    
                                    <v-text-field color="#2a2d45" outlined dense v-model="CatPrefixCode" label="پیشوند کد" ></v-text-field>
                                   
                                    <v-text-field color="#2a2d45" outlined dense v-model="CatSortField" label="ترتیب نمایش"></v-text-field>
                                   
                                    <vue-editor placeholder="راهنمای خرید مطمئن " class="mb-10" v-model="CatDescription"></vue-editor>

                                    <v-row>
                                        <v-col cols="8">وضعیت :</v-col>
                                        <v-col cols="4" style="display: flex;justify-content: flex-end;">
                                            <v-switch v-model="CatStatus" style="padding-top:0px !important;margin-top:0px !important" color="#2a2d45"></v-switch>
                                        </v-col>
                                    </v-row>
                                   
                                    <v-row>
                                        <v-col cols="5"><v-img contain width="48" class="PreviewLogoCropped" :src="catLogoData"></v-img></v-col>
                                        <v-col cols="7" style="display: flex;justify-content: flex-end;">
                                            <input accept="image/png,image/jpg,image/jpeg,image/gif" @change="catLogoSelectedEvt($event)" type="file" ref="catLogoSelectorRef" style="display:none"/>
                                            <v-btn outlined dark color="#7600b3" @click="$refs.catLogoSelectorRef.click()"><v-icon small style="padding-left:5px;">fa fa-image</v-icon>&nbsp; لوگو</v-btn>
                                        </v-col>
                                    </v-row>
                                  
                                    <v-btn @click="registerCategory" outlined dark block color="blue"><v-icon small style="padding-left:5px;">fa fa-check</v-icon>&nbsp; ذخیره</v-btn>
                                </v-flex>
                                <v-flex xs12 sm12 md8 lg8 xl8 style="padding:15px;padding-top:0px !important"><!--Seo Form-->
                                    <seo-form FileLoc="PRDCATSEOIMG" :Seo="SeoData" @SeoData="SeoChanged"></seo-form>
                                </v-flex>
                            </v-layout>
                        </v-flex>
                      </v-layout>
                  </v-card-text>
                </v-card>
            </v-dialog>
        </template>
        <template><!--Category Tree View-->
            <v-dialog fullscreen persistent v-model="CategoryTreeViewDialog">
                <v-card>
                  <v-card-title style="padding:0px !important">
                      <v-app-bar dense color="rgb(42, 45, 69)" dark>
                          <v-toolbar-title style="color:orange">ساختار درختی دسته بندی ها</v-toolbar-title>
                          <v-spacer></v-spacer>
                          <v-icon color="orange" @click="CategoryTreeViewDialog = false" small>fa fa-times</v-icon>
                      </v-app-bar>
                  </v-card-title>
                  <v-card-text>
                      <v-card>
                          <v-treeview shaped hoverable :items="CategoryTreeViewItems" item-key="pC_Id" item-text="pC_Title">

                          </v-treeview>
                      </v-card>
                  </v-card-text>
                </v-card>
            </v-dialog>
        </template>
        <template><!--Image Cropper Dialog-->
            <v-dialog max-width="600" style="overflow: hidden !important;" persistent v-model="catLogoSelectionDialog">
                <v-card>
                    <v-card-title style="padding:0px !important">
                        <v-app-bar dense color="rgb(42, 45, 69)" dark>
                            <v-toolbar-title style="color:orange">برش عکس</v-toolbar-title>
                            <v-spacer></v-spacer>
                            <v-icon color="orange" @click="catLogoSelectionDialog = false" small>fa fa-times</v-icon>
                        </v-app-bar>
                    </v-card-title>
                    <v-card-text style="padding-top:10px;">
                        <v-alert dense type="info" outlined>
                            <div>کاربر گرامی ؛ </div>
                            <div>اندازه عکس دسته بندی بهتر است به صورت زیر باشد :</div>
                            <div>کمینه عرض : {{ALLOEWDIMGSIZE.minwidth}} پیکسل</div>
                            <div>بیشینه عرض : {{ALLOEWDIMGSIZE.maxwidth}} پیکسل</div>
                            <div>کمینه ارتفاع : {{ALLOEWDIMGSIZE.minheight}} پیکسل</div>
                            <div>بیشینه ارتفاع : {{ALLOEWDIMGSIZE.maxheight}} پیکسل</div>
                            <div>نسبت عرض به ارتفاع : {{ALLOEWDIMGSIZE.aspectratio}}</div>
                        </v-alert>
                        <div class="overFlowHidden">
                            <cropper previewClassname="PreviewLogoCropped" maxWidth="400" maxHeight="400" classname="cropper" :src="selectedLogoSource"  @change="change"></cropper>
                        </div>
                    </v-card-text>
                    <v-divider></v-divider>
                    <v-card-actions>
                        <v-btn outlined dark color="blue" @click="uploadEntityLogo"><v-icon small style="padding-left:5px;">fa fa-upload</v-icon>&nbsp; آپلود</v-btn>&nbsp;
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </template>
        <template><!--Loading Dialog-->
            <div class="vld-parent">
                <loading color="#f75314" :width="parseInt(45)" :height="parseInt(45)" background-color="#fff" :active.sync="isLoading" 
                :is-full-page="true"></loading>
                
               
            </div>
        </template>
        <template><!--Delete Confirm Dialog-->
            <v-dialog max-width="450" persistent v-model="ConfirmItemDeleteDialog">
                <v-card>
                    <v-card-title style="padding:0px !important">
                        <v-app-bar dense color="rgb(42, 45, 69)" dark>
                            <v-toolbar-title style="color:orange">حذف دائمی</v-toolbar-title>
                            <v-spacer></v-spacer>
                            <v-icon color="orange" @click="ConfirmItemDeleteDialog = false" small>fa fa-times</v-icon>
                        </v-app-bar>
                    </v-card-title>
                    <v-card-text style="padding-top:15px;">
                        <v-alert type="error" outlined>
                            کاربر گرامی ؛ رکورد مورد نظر به همراه تمام زیر مجموعه های آن به صورت دائمی حذف خواهند شد.ادامه میدهید؟
                        </v-alert>
                    </v-card-text>
                    <v-divider></v-divider>
                    <v-card-actions>
                        <v-btn outlined :loading="deleteBtnLoading" color="error" @click="DeleteItem"><v-icon small style="padding-left:5px;">fa fa-trash</v-icon>&nbsp; بله</v-btn>&nbsp;
                        <v-btn outlined @click="ConfirmItemDeleteDialog = false" color="info"><v-icon small style="padding-left:5px;">fa fa-times</v-icon>&nbsp; خیر</v-btn>&nbsp;
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </template>
        <template><!--Delete Attribute Confirm Dialog-->
            <v-dialog max-width="450" persistent v-model="ConfirmAttrDeleteDialog">
                <v-card>
                    <v-card-title style="padding:0px !important">
                        <v-app-bar dense color="rgb(42, 45, 69)" dark>
                            <v-toolbar-title style="color:orange">حذف دائمی</v-toolbar-title>
                            <v-spacer></v-spacer>
                            <v-icon color="orange" @click="ConfirmAttrDeleteDialog = false" small>fa fa-times</v-icon>
                        </v-app-bar>
                    </v-card-title>
                    <v-card-text style="padding-top:15px;">
                        <v-alert type="error" outlined>
                            کاربر گرامی ؛ رکورد مورد نظر به همراه تمام زیر مجموعه های آن به صورت دائمی حذف خواهند شد.ادامه میدهید؟
                        </v-alert>
                    </v-card-text>
                    <v-divider></v-divider>
                    <v-card-actions>
                        <v-btn outlined :loading="deleteBtnLoading" color="error" @click="DeleteItem"><v-icon small style="padding-left:5px;">fa fa-trash</v-icon>&nbsp; بله</v-btn>&nbsp;
                        <v-btn outlined @click="ConfirmAttrDeleteDialog = false" color="info"><v-icon small style="padding-left:5px;">fa fa-times</v-icon>&nbsp; خیر</v-btn>&nbsp;
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </template>
        <template><!--Delete AttrAnswer Confirm Dialog-->
            <v-dialog max-width="450" persistent v-model="ConfirmAttrAnswerDeleteDialog">
                <v-card>
                    <v-card-title style="padding:0px !important">
                        <v-app-bar dense color="rgb(42, 45, 69)" dark>
                            <v-toolbar-title style="color:orange">حذف دائمی</v-toolbar-title>
                            <v-spacer></v-spacer>
                            <v-icon color="orange" @click="ConfirmAttrAnswerDeleteDialog = false" small>fa fa-times</v-icon>
                        </v-app-bar>
                    </v-card-title>
                    <v-card-text style="padding-top:15px;">
                        <v-alert type="error" outlined>
                            کاربر گرامی ؛ رکورد مورد نظر به همراه تمام زیر مجموعه های آن به صورت دائمی حذف خواهند شد.ادامه میدهید؟
                        </v-alert>
                    </v-card-text>
                    <v-divider></v-divider>
                    <v-card-actions>
                        <v-btn outlined :loading="deleteBtnLoading" color="error" @click="DeleteAttrAnswer"><v-icon small style="padding-left:5px;">fa fa-trash</v-icon>&nbsp; بله</v-btn>&nbsp;
                        <v-btn outlined @click="ConfirmAttrAnswerDeleteDialog = false" color="info"><v-icon small style="padding-left:5px;">fa fa-times</v-icon>&nbsp; خیر</v-btn>&nbsp;
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </template>
        <template><!--Set Attributes for Category-->
            <v-dialog fullscreen persistent v-model="SetCatAttributeDialog">
                <v-card>
                    <v-card-title style="padding:0px">
                        <v-app-bar dense color="rgb(42, 45, 69)" dark>
                            <v-toolbar-title style="color:orange">
                                <span v-if="!inEditingItem">تنظیم فیلد های اختصاصی دسته بندی</span>
                                <span v-else>ویرایش دسته بندی  &nbsp; <v-btn small outlined color="red" @click="cancelEditItem"><v-icon small style="padding-left:5px;">fa fa-times</v-icon>&nbsp;لغو ویرایش</v-btn></span>
                            </v-toolbar-title>
                            <v-spacer></v-spacer>
                            <v-icon color="orange" @click="SetCatAttributeDialog = false" small>fa fa-times</v-icon>
                        </v-app-bar>
                    </v-card-title>
                    <v-card-text>
                        <v-layout row wrap>
                            <v-flex xs12 sm12 md4 lg4 xl4 style="padding:20px;">
                                <v-autocomplete :items="AttributeItems" @change="AttributeChanged($event)" item-text="a_Title" item-value="a_Id" no-data-text="فیلد اختصاصی یافت نشد" v-model="Attribute" color="#2a2d45" outlined dense label="فیلد اختصاصی"></v-autocomplete>
                                <v-row v-if="IsComboBox">
                                    <v-col cols="8">
                                        افزایش / کاهش ورودی
                                    </v-col>
                                    <v-col cols="4">
                                        <v-btn small color="success" text @click="CountOfTextBox++"><v-icon small>fas fa-chevron-up</v-icon></v-btn>
                                        <span>{{CountOfTextBox}}</span>
                                        <v-btn small color="error" text @click="(CountOfTextBox > 1)?CountOfTextBox--:''"><v-icon small>fas fa-chevron-down</v-icon></v-btn>
                                    </v-col>
                                </v-row>
                                <div v-if="IsComboBox">
                                    <v-row  v-for="i in CountOfTextBox" :key="'Row'+i">
                                        <v-col cols="8">
                                            <v-text-field color="#2a2d45" outlined dense v-model="AttrResponse.resps[i]" label="پاسخ"></v-text-field>
                                        </v-col>
                                        <v-col cols="4">
                                            <v-text-field color="#2a2d45" outlined dense v-model="AttrResponse.sort[i]" label="ترتیب نمایش"></v-text-field>
                                        </v-col>
                                    </v-row> 
                                </div>

                                <v-btn @click="SetCategoryAttribute" outlined dark block color="blue"><v-icon small style="padding-left:5px;">fa fa-check</v-icon>&nbsp; ذخیره</v-btn>
                            </v-flex>
                            <v-flex xs12 sm12 md4 lg4 xl4 style="padding:20px;">
                                <v-card>
                                    <v-card-title>
                                        فیلد های اختصاصی
                                    </v-card-title>
                                    <v-card-text>
                                        <v-data-table :headers="AttributeTableHeaders" :items="AttribteTableItems" no-data-text="رکوردی یافت نشد" no-results-text="رکوردی یافت نشد" :footer-props="{'items-per-page-text':'آیتم در هر صفحه :','items-per-page-all-text':'همه'}"> 
                                            <template v-slot:[`item.operation`]="{ item }">
                                                <v-btn text icon small color="purple accent-4" @click="getAnswersByRelationId(item.relationId,true)"><v-icon small>fas fa-eye</v-icon></v-btn>&nbsp;
                                                
                                                <v-btn text icon small color="red accent-4" @click="confirmDeleteAttribute(item.pC_Id)"><v-icon small>fas fa-trash</v-icon></v-btn>&nbsp;
                                            </template>
                                            <template v-slot:[`item.a_Status`]="{ item }">
                                                <v-btn small text color="#D32F2F" @click="ActiveItem(item.relationId)" dark v-if="!item.a_Status">غیر فعال</v-btn>
                                                <v-btn small text color="#2E7D32" @click="DisableItem(item.relationId)" dark v-else> فعال</v-btn>
                                            </template>
                                        </v-data-table>
                                    </v-card-text>
                                </v-card>
                            </v-flex>
                            <v-flex xs12 sm12 md4 lg4 xl4 style="padding:20px;">
                                <v-card>
                                    <v-card-title>
                                        پاسخ ها
                                        <v-spacer></v-spacer>
                                        <v-btn outlined @click="AddAnswerDialogShow" color="green" small><v-icon small>fa fa-plus</v-icon></v-btn>
                                    </v-card-title>
                                    <v-card-text>
                                        <v-data-table :items="AnswerTableItems" :headers="AttributeAnswerTableHeaders" no-data-text="رکوردی یافت نشد" no-results-text="رکوردی یافت نشد" :footer-props="{'items-per-page-text':'آیتم در هر صفحه :','items-per-page-all-text':'همه'}"> 
                                            <template v-slot:[`item.operation`]="{ item }">
                                                <v-btn text icon small color="red accent-4" @click="confirmDeleteAttrAnswer(item.x_Id)"><v-icon small>fas fa-trash</v-icon></v-btn>&nbsp;
                                                <v-btn text icon small color="blue accent-4" @click="editCatAttrItem(item)"><v-icon small>fas fa-edit</v-icon></v-btn>
                                            </template>
                                            <template v-slot:[`item.x_Status`]="{ item }">
                                                <v-btn small text color="#D32F2F" @click="ActiveAnswerItem(item.x_Id)" dark v-if="!item.x_Status">غیر فعال</v-btn>
                                                <v-btn small text color="#2E7D32" @click="DisableAnswerItem(item.x_Id)" dark v-else> فعال</v-btn>
                                            </template>
                                        </v-data-table>
                                    </v-card-text>
                                </v-card>
                            </v-flex>
                        </v-layout>
                    </v-card-text>
                </v-card>
            </v-dialog>
        </template>
        <template>
            <v-dialog v-model="EditAnswerDialog" max-width="800">
                <v-card>
                    <v-card-text>
                        <v-layout row wrap align-center>
                            <v-flex xs12 sm12 md12 lg12 xl12 style="padding:20px">
                                <v-row>
                                    <v-col cols="8">
                                        <v-text-field color="#2a2d45" outlined dense v-model="EditItem_Answer" label="پاسخ"></v-text-field>
                                    </v-col>
                                    <v-col cols="4">
                                        <v-text-field color="#2a2d45" outlined dense v-model="EditItem_Sort" label="ترتیب نمایش"></v-text-field>
                                    </v-col>
                                </v-row>
                            </v-flex>
                        </v-layout>
                    </v-card-text>
                    <v-card-actions>
                        <v-btn outlined :loading="updatingAnswer" @click="UpdateAttributeAnswer" color="orange">بروزرسانی</v-btn> &nbsp;
                        <v-btn outlined @click="EditAnswerDialog = false" color="blue">انصراف</v-btn> &nbsp;
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </template>
        <template>
            <v-dialog v-model="AddAnswerDialog" max-width="800">
                <v-card>
                    <v-card-text>
                        <v-layout row wrap align-center>
                            <v-flex xs12 sm12 md12 lg12 xl12 style="padding:20px">
                                <v-row>
                                    <v-col cols="8">
                                        <v-text-field color="#2a2d45" outlined dense v-model="AddItem_Answer" label="پاسخ"></v-text-field>
                                    </v-col>
                                    <v-col cols="4">
                                        <v-text-field color="#2a2d45" outlined dense v-model="AddItem_Sort" label="ترتیب نمایش"></v-text-field>
                                    </v-col>
                                </v-row>
                            </v-flex>
                        </v-layout>
                    </v-card-text>
                    <v-card-actions>
                        <v-btn outlined :loading="addingAnswer" @click="AddAttributeAnswer" color="orange">بروزرسانی</v-btn> &nbsp;
                        <v-btn outlined @click="AddAnswerDialog = false" color="blue">انصراف</v-btn> &nbsp;
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </template>
        <template>
            <v-dialog fullscreen persistent v-model="SetPromotionBoxDialog">
                <v-card>
                    <v-card-title style="padding: 0px !important">
                        <v-app-bar dense color="rgb(42, 45, 69)" dark>
                        <v-toolbar-title style="color: orange">
                            <span>مدیریت باکس</span>
                        </v-toolbar-title>
                        <v-spacer></v-spacer>
                        <v-icon color="orange" @click="$router.go($router.currentRoute)" small
                            >fa fa-times</v-icon
                        >
                        </v-app-bar>
                    </v-card-title>
                    <v-card-text>
                        <PromotionBoxs :EntityId="EntityId" :Type="BoxType"></PromotionBoxs>
                    </v-card-text>
                </v-card>
            </v-dialog>
        </template>
    </div>
</template>

<style scoped>
.overFlowHidden .v-dialog:not(.v-dialog--fullscreen)
{
    overflow: hidden !important;
}
.overFlowHidden 
{
    max-width: 400px;
    max-height: 400px;
    overflow: hidden !important;
    display: flex;
    justify-content: center;
    align-items: center;
    margin: auto;
}
.operationBoxDesign
{
    display: flex;
    align-items: center;
    border-radius: 0px;
    justify-content: space-evenly;
    min-height: 50px;
    width: 100%;
    padding-top: 10px;
    padding-bottom: 10px;
    padding-right: 10px;
    padding-left: 10px;
    /* border: 1px solid rgb(207, 207, 207); */
    background-color: #fff;
    margin-bottom: 4px;
    box-shadow: 0px 0px 4px rgba(207, 207, 207,0.5);
}
</style>
<script>
import PromotionBoxs from '../../../components/forms/PromotionBox';
import { VueEditor } from "vue2-editor";
import { mapState, mapMutations, mapActions } from 'vuex'
const axios = require("axios");
const ApiUrl = require('../../../constant/apiurl');
const MSGHNDL = require('../../../constant/MessageHandler');
const FTYPES = require('../../../constant/setting/Default/AllowedFileTypes');
const FSIZE = require('../../../constant/setting/Default/AllowedFileSize');
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';
import JsonExcel from 'vue-json-excel'
export default {
    name:"productCategory",
   components: {
            Loading,
            JsonExcel,
            VueEditor,
            PromotionBoxs
        },
    data:()=> ({
        EntityId:0,
        BoxType:3,
        SetPromotionBoxDialog:false,
        ConfirmAttrAnswerDeleteDialog:false,
        ConfirmItemDeleteDialog:false,
        isLoading:false,
        SetCatAttributeDialog:false,
        selectedLogoSource: '',
        catLogoSelectionDialog:false,
        CatCreationDialog:false,
        CatImageName:"",
        searchcategory:"",
        isLoadingCats:false,
        CatTableHeader:
        [
            {text:"#",value:"counter",sortable:true,align:"right"},
            {text:" دسته بندی",value:"pC_ParentTitle",sortable:true,align:"right"},
            {text:"عنوان ",value:"pC_Title",sortable:true,align:"right"},
            {text:"سطح ",value:"pC_Level",sortable:true,align:"right"},
            {text:"ترتیب نمایش",value:"pC_OrderField",sortable:true,align:"right"},
            {text:"لوگو",value:"pC_Logo",sortable:true,align:"right"},
            {text:"وضعیت",value:"pC_Status",sortable:true,align:"right"},
            {text:"عملیات",value:"operation",sortable:true,align:"right"},
        ],
        CatTableItems:
        [
        ],
        ParentCat:"",
        CatTitle:"",
        CatSortField:"",
        CatStatus:"",
        seo:{},
        filterlevel:0,
        flilteredStatus:false,
        filterLevelList:[],
        originalItems:[],
        CategoryTreeViewDialog:false,
        CategoryTreeViewItems:false,
        catLogoData:require('../../../assets/logo.png'),
        ALLOEWDIMGSIZE:{},
        LogoUrl:"",
        CatSeoModel:{},
        inEditingItem:false,
        EditSelectedItem:{},
        deleteItemId:0,
        deleteBtnLoading:false,
        ExportDataColumns:{
            "دسته بندی اصلی":"pC_ParentTitle",
            "عنوان":"pC_Title",
            "سطح":"pC_Level",
            "ترتیب نمایش":"pC_OrderField",
            "وضعیت":"pC_Status",
            "فایل لوگو":"pC_Logo",
            "تنظیمات سئو":"pC_Seo",
        },
        RegisteredLevelsPercentage:0,
        MaxLevelCount:0,
        MaxUsedLevelCount:0,
        ////attribute dialog content setting
        AttributeItems:[],
        CountOfTextBox:1,
        AttrResponse:{
            resps:[],
            sort:[]
        },
        Attribute:"",
        IsComboBox:false,
        selectedCatId:0,
        AttributeTableHeaders:[
            {text:"#",value:"counter",align:"right",sortable:true},
            {text:"عنوان",value:"a_Title",align:"right",sortable:true},
            {text:"وضعیت",value:"a_Status",sortable:true,align:"right"},
            {text:"عملیات",value:"operation",align:"right",sortable:true},
        ],
        AttributeAnswerTableHeaders:[
            {text:"#",value:"counter",align:"right",sortable:true},
            {text:"عنوان",value:"x_Answer",align:"right",sortable:true},
            {text:"ترتیب نمایش",value:"x_Sort",align:"right",sortable:true},
            {text:"وضعیت",value:"x_Status",align:"right",sortable:true},
            {text:"عملیات",value:"operation",align:"right",sortable:true},
        ],
        AttribteTableItems:[],
        editedAnswerIds:[],
        inEditingAttr:false,
        editCatAttrId:0,
        AnswerTableItems:[],
        savedCatId:0,
        ConfirmAttrDeleteDialog:false,
        SeoData:{},
        CatImgTitle:"",
        CatPrefixCode:"",
        CatDescription:"",
        /**********************CATEGORY ATTRIBUTE CHANGE SETTING */
        SeletedAttributeID:0,
        SelectedAttrAnswer:{X_CatAttrId:0,X_Id:0},
        EditItem_Answer:"",
        EditItem_Sort:"",
        EditAnswerDialog:false,
        updatingAnswer:false,

        AddAnswerDialog:false,
        AddItem_Answer:"",
        AddItem_Sort:"",
        addingAnswer:false

    }),
    async created()
    {
        let vm = this;
        vm.LogoUrl = ApiUrl.PcLogo;
       axios.interceptors.request.use((config) => {
            vm.isLoading = true;
            return config;
        }, (error) => {
            vm.isLoading = false;
            return Promise.reject(error);
        });

        axios.interceptors.response.use((response) => {
            vm.isLoading = false;
            return response;
        }, (error) => {
            vm.isLoading = false;
            return Promise.reject(error);
        });
       await this.loadCategories();
       this.ALLOEWDIMGSIZE = FSIZE.PRDCATIMGSIZE;
    },
    computed:{
        LANG(){
        return this.$store.getters.GetLang;
        }
    },
    watch:{
        LANG(nL,oL){
        this.loadCategories();
        }
    },
    methods:{
        MoveableAds(eid)
        {
            let vm = this;
            vm.EntityId = eid;
            vm.BoxType=5;
            vm.SetPromotionBoxDialog=true;
        },
        StaticAds(eid)
        {
            let vm = this;
            vm.EntityId = eid;
            vm.BoxType=4;
            vm.SetPromotionBoxDialog=true;
        },
        DsicountedBoxSetting(eid)
        {
            let vm = this;
            vm.EntityId = eid;
            vm.BoxType=3;
            vm.SetPromotionBoxDialog=true;
        },
        ...mapActions([                  // Add this
        'setSeoForm'
        ]),
        LoadSeoFormData()
        {
            let vm = this;

            return vm.seo;
           
        },
        async getAnswersByRelationId(catid,ViewBtnClicked = false)
        {
            let vm = this;
            vm.savedCatId = catid;
            vm.SeletedAttributeID = catid;
            await axios.post(ApiUrl.LoadAnswerByAttCatId+'/'+catid).then(async (res)=>{
                let data = res.data;
                console.warn(data);
                if(data.status)
                {
                    console.error(data.data);
                   if(data.data.length > 0 && !ViewBtnClicked)
                   {
                       vm.IsComboBox = true;
                       vm.CountOfTextBox = data.data.length;
                       vm.AttrResponse.resps.push(null);
                       vm.AttrResponse.sort.push(null);
                       data.data.forEach((item)=>{
                           console.log(item);
                           vm.editedAnswerIds.push(item.x_Id);
                           vm.AttrResponse.resps.push(item.x_Answer);
                           vm.AttrResponse.sort.push(item.x_Sort);
                       });
                   }
                   else
                   {
                       vm.AnswerTableItems = data.data;
                   }
                }
            }).catch((err)=>{
                console.error(err);
            });
        },
        async getAttributesByCategory(catid)
        {
            let vm = this;
            await axios.post(ApiUrl.LoadAttrsByCategory+'/'+catid).then(async (res)=>{
                let data = res.data;
                console.warn(data);
                if(data.status)
                {
                    vm.AttribteTableItems = data.data;
                }
            }).catch((err)=>{
                console.error(err);
            });
        },
        async SetCategoryAttribute()
        {
            let vm = this;
            let Model = {};
            let url = ApiUrl.SetAttrToCategory;
            if(vm.IsComboBox)
            {
                vm.AttrResponse.resps.shift();
                vm.AttrResponse.sort.shift();
                Model.X_Answers = vm.AttrResponse.resps.join(',')+":"+vm.AttrResponse.sort.join(',');
            }
            Model.X_Type = vm.IsComboBox ? 1:2;
            Model.X_AttrId = vm.Attribute;
            Model.X_CatId = vm.selectedCatId;
            if(vm.inEditingAttr)
            {
                Model.X_AnswersIds = vm.editedAnswerIds;
                Model.X_Id = vm.editCatAttrId;
                url = ApiUrl.updateCatAttribute;
            }
            await axios.post(url,Model)
            .then(async (res)=>{
                
                let data = res.data;
                if(data.status)
                {
                    MSGHNDL.OK(data.msg);
                    vm.ConfirmItemDeleteDialog = false;
                }
                else
                {
                    MSGHNDL.ERR(data.msg);
                }
            })
            .catch((err)=>{
                MSGHNDL.ERR(err);
            });

        },
        async AttributeDialogShow(id)
        {
            let vm = this;
            vm.selectedCatId = id;
            vm.SetCatAttributeDialog = true;
            await vm.getAttributesByCategory(id);
        },
        AttributeChanged(e)
        {
            let vm = this;
            vm.AttributeItems.forEach(item=>{
                if(item.a_Id == e)
                {
                    vm.IsComboBox = (item.a_Type == 1);
                }
            });
        },
        async DeleteAttrAnswer()
        {
            let vm = this;
            vm.deleteBtnLoading = true;
            let Model = {};
            Model.X_Id = vm.deleteItemId;
            await axios.post(ApiUrl.DeleteAttrAnswer,Model)
            .then(async (res)=>{
                vm.deleteBtnLoading = false;
                let data = res.data;
                if(data.status)
                {
                    MSGHNDL.OK(data.msg);
                    vm.ConfirmAttrAnswerDeleteDialog = false;
                    await vm.getAnswersByRelationId(vm.SeletedAttributeID,true);
                }
                else
                {
                    MSGHNDL.ERR(data.msg);
                }
            })
            .catch((err)=>{
                vm.deleteBtnLoading = false;
                MSGHNDL.ERR(err);
            });
        },
        async DeleteItem()
        {
            let vm = this;
            vm.deleteBtnLoading = true;
            let Model = {};
            Model.PC_Id = vm.deleteItemId;
            await axios.post(ApiUrl.DeletePC,Model)
            .then(async (res)=>{
                vm.deleteBtnLoading = false;
                let data = res.data;
                if(data.status)
                {
                    MSGHNDL.OK(data.msg);
                    vm.ConfirmItemDeleteDialog = false;
                    await vm.loadCategories();
                }
                else
                {
                    MSGHNDL.ERR(data.msg);
                }
            })
            .catch((err)=>{
                vm.deleteBtnLoading = false;
                MSGHNDL.ERR(err);
            });
        },
        confirmDeleteAttrAnswer(id)
        {
            let vm = this;
            vm.ConfirmAttrAnswerDeleteDialog = true;
            vm.deleteItemId = id;
        },
        confirmDeleteAttribute(id)
        {
            let vm = this;
            vm.ConfirmAttrDeleteDialog = true;
            vm.deleteItemId = id;
        },
        confirmDeleteItem(id)
        {
            let vm = this;
            vm.ConfirmItemDeleteDialog = true;
            vm.deleteItemId = id;
        },
        async ActiveAnswerItem(id)
        {
            let vm = this;
            let Model = {};
            Model.X_Id = id;
            await axios.post(ApiUrl.ActiveAttrAnswer,Model)
            .then(async (res)=>{
                let data = res.data;
                if(data.status)
                {
                    MSGHNDL.OK(data.msg);
                    await vm.getAnswersByRelationId(vm.savedCatId,true);
                }
                else
                {
                    MSGHNDL.ERR(data.msg);
                }
            })
            .catch((err)=>{
                MSGHNDL.ERR(err);
            });
        },
        async ActiveItem(id)
        {
            let vm = this;
            let Model = {};
            Model.PC_Id = id;
            await axios.post(ApiUrl.ActivePC,Model)
            .then(async (res)=>{
                let data = res.data;
                if(data.status)
                {
                    MSGHNDL.OK(data.msg);
                    await vm.loadCategories();
                }
                else
                {
                    MSGHNDL.ERR(data.msg);
                }
            })
            .catch((err)=>{
                MSGHNDL.ERR(err);
            });
        },
        async DisableItem(id)
        {
            let vm = this;
            let Model = {};
            Model.PC_Id = id;
            await axios.post(ApiUrl.DisablePC,Model)
            .then(async (res)=>{
                let data = res.data;
                if(data.status)
                {
                    MSGHNDL.OK(data.msg);
                    await vm.loadCategories();
                }
                else
                {
                    MSGHNDL.ERR(data.msg);
                }
            })
            .catch((err)=>{
                MSGHNDL.ERR(err);
            });
        },
        async DisableAnswerItem(id)
        {
            let vm = this;
            let Model = {};
            Model.X_Id = id;
            await axios.post(ApiUrl.DisableAttrAnswer,Model)
            .then(async (res)=>{
                let data = res.data;
                if(data.status)
                {
                    MSGHNDL.OK(data.msg);
                   await vm.getAnswersByRelationId(vm.savedCatId,true);
                }
                else
                {
                    MSGHNDL.ERR(data.msg);
                }
            })
            .catch((err)=>{
                MSGHNDL.ERR(err);
            });
        },
        cancelEditItem()
        {
            let vm = this;
            vm.CatCreationDialog = false;
            vm.ParentCat = "";
            vm.CatTitle = "";
            vm.CatSortField = "";
            vm.CatStatus = false;
            vm.catLogoData = require('../../../assets/logo.png');
            vm.seo = {
            title:"",
            url:"",
            keywords:"",
            image:"",
            description:"",
            othermeta:""
            };
            vm.CatImgTitle="";
            vm.CatPrefixCode="";
            vm.CatDescription="";
            vm.inEditingItem = false;
            vm.EditSelectedItem = {};
        },
        AddAnswerDialogShow()
        {
            let vm = this;
            vm.AddAnswerDialog = true;
        },
        async AddAttributeAnswer()
        {
            let vm = this;
            var Model = {};
            Model.X_CatAttrId = vm.SeletedAttributeID;
            Model.X_Answer = vm.AddItem_Answer;
            Model.X_Sort = vm.AddItem_Sort;
            vm.addingAnswer = true;
            await axios.post(ApiUrl.AddAttrAnswer,Model)
            .then(async (res)=>{
                var data = res.data;
                if(data.status)
                {
                    MSGHNDL.OK(data.msg);
                    vm.getAnswersByRelationId(vm.SeletedAttributeID,true);
                    vm.AddAnswerDialog = false;
                }
                else
                {
                    MSGHNDL.ERR(data.msg);
                }
                vm.addingAnswer = false;
            })
            .catch((err)=>{
                vm.addingAnswer = false;
                console.error(err);
            });

        },
        async UpdateAttributeAnswer()
        {
            let vm = this;
            var Model = {};
            Model.X_Id = vm.SelectedAttrAnswer.X_Id;
            Model.X_Answer = vm.EditItem_Answer;
            Model.X_Sort = vm.EditItem_Sort;
            vm.updatingAnswer = true;
            await axios.post(ApiUrl.UpdateAttrAnswer,Model)
            .then(async (res)=>{
                var data = res.data;
                if(data.status)
                {
                    MSGHNDL.OK(data.msg);
                    vm.getAnswersByRelationId(vm.SeletedAttributeID,true);
                    vm.EditAnswerDialog = false;
                }
                else
                {
                    MSGHNDL.ERR(data.msg);
                }
                vm.updatingAnswer = false;
            })
            .catch((err)=>{
                vm.updatingAnswer = false;
                console.error(err);
            });

        },
        async editCatAttrItem(item)
        {
            
            let vm = this;
            vm.SelectedAttrAnswer.X_CatAttrId = item.x_CatAttrId;
            vm.SelectedAttrAnswer.X_Id = item.x_Id;
            vm.EditItem_Answer = item.x_Answer;
            vm.EditItem_Sort = item.x_Sort;
            vm.EditAnswerDialog = true;
        },
        editItem(item)
        {
            let vm = this;
            vm.CatCreationDialog = true;
            vm.ParentCat = item.pC_ParentId;
            vm.CatTitle = item.pC_Title;
            vm.CatSortField = item.pC_OrderField;
            vm.CatStatus = item.pC_Status;
            vm.catLogoData = vm.LogoUrl+item.pC_Logo;
            vm.CatImgTitle = item.pC_ImgTitle;
            vm.CatPrefixCode = item.pC_PrefixCode;
            vm.CatDescription = item.pC_Description ;
            vm.SeoData = (JSON.parse(item.pC_Seo));

            vm.inEditingItem = true;
            vm.EditSelectedItem = item;
        },
        async uploadEntityLogo()
        {
            let vm = this;
            await axios.post(ApiUrl.registerPcImg,{DataUrl:vm.catLogoData,Loc : "PRDCATIMG"}).then((res)=>{
                if(res.data.status)
                {
                    MSGHNDL.OK(res.data.msg);
                    vm.CatImageName = res.data.data;
                    vm.catLogoSelectionDialog = false;
                }
                else
                {
                    MSGHNDL.ERR(res.data.msg);
                }
            }).catch((err)=>{console.error(err);});
        },
        catLogoSelectedEvt(e)
        {
            let vm = this;
            let file = e.target.files[0];
            if(FTYPES.PRDCATLOGOFILETYPES.indexOf(file.type) == -1)
            {
                MSGHNDL.ERRFILEFORMAT();
                e.preventDefault();
                return;
            }
            if(file.size > 2000000)
            {
                MSGHNDL.ERRFILESIZE("2 مگابایت ");
                e.preventDefault();
                return;
            }
            MSGHNDL.CHKIMAGESIZE(file,0,0,0,0,1);
            
            if (FileReader) {
                var fr = new FileReader();
                fr.onload = function () {
                    vm.selectedLogoSource = fr.result;
                    vm.catLogoSelectionDialog = true;
                }
                fr.readAsDataURL(file);
            }
        },
        change({coordinates, canvas}) {
           this.catLogoData = canvas.toDataURL();
        },
        list_to_tree(arr) {
            var tree = [],
            mappedArr = {},
            arrElem,
            mappedElem;
            for(var i = 0, len = arr.length; i < len; i++) {
                arrElem = arr[i];
                mappedArr[arrElem.pC_Id] = arrElem;
                mappedArr[arrElem.pC_Id]['children'] = [];
            }
            for (var id in mappedArr) {
                if (mappedArr.hasOwnProperty(id)) {
                    mappedElem = mappedArr[id];
                    // If the element is not at the root level, add it to its parent array of children.
                    if (mappedElem.pC_ParentId) {
                        mappedArr[mappedElem['pC_ParentId']]['children'].push(mappedElem);
                    }
                    // If the element is at the root level, add it to first level elements array.
                    else {
                        tree.push(mappedElem);
                    }
                }
            }
            return tree;
        },
        CategoryTreeView()
        {
            let vm = this;
            vm.CategoryTreeViewDialog = true;
        },
        clearFilter()
        {
            let vm = this;
            vm.CatTableItems = vm.originalItems;
            vm.filterlevel = 0;
            vm.flilteredStatus = false;
        },
        async filterCats(e,opr = "")
        {
            let vm = this;
            if(vm.originalItems.length > 0)
            {
                var filteredItems = [];
                vm.originalItems.forEach((item)=>{
                   if(opr == "")
                   {
                        if(item.pC_Level == vm.filterlevel)
                        {
                            filteredItems.push(item);
                        }
                   }
                   else if(opr == "switch")
                   {
                       if(item.pC_Status == e)
                        {
                            filteredItems.push(item);
                            vm.flilteredStatus = true;
                        }
                   }
                });
                vm.CatTableItems = filteredItems;
            }
        },
        async loadCategories()
        {
            let vm = this;
            vm.isLoadingCats = true;

            await axios.post(ApiUrl.LoadProdCats+'/0/'+vm.LANG).then(async (res)=>{
                vm.isLoadingCats = false;
                let data = res.data;
                console.warn(data);
                if(data.status)
                {
                    vm.CatTableItems = data.data;
                    data.data.forEach(function(item){
                        if(item.pC_Id != vm.EditSelectedItem.pC_Id)
                        {
                            vm.originalItems.push(item);
                        }
                    });
                    let max = Math.max.apply(Math, vm.CatTableItems.map(function(o) { return o.pC_Level; }));
                    vm.MaxUsedLevelCount = max;
                    console.warn(data.data);
                    for(let i=1;i<=max;i++)
                    {
                        vm.filterLevelList.push({text:i,value:i});
                    }
                    vm.CategoryTreeViewItems = vm.list_to_tree(vm.originalItems);
                    vm.getAllowedMaxLevelCount();
                }
            }).catch((err)=>{
                vm.isLoadingCats = false;
                console.error(err);
            });
        },
        async getAllowedMaxLevelCount()
        {
            let vm = this;
            await axios.post(ApiUrl.LoadProdCatsMaxLvlCount).then(async (res)=>{
                let data = res.data;
                console.warn(data);
                if(data.status)
                {
                    vm.MaxLevelCount = data.data;
                    if(vm.MaxUsedLevelCount > 0)
                    {
                        vm.RegisteredLevelsPercentage = ((vm.MaxUsedLevelCount / vm.MaxLevelCount) * 100);
                    }
                }
                await vm.getAttrsForCombo();
            }).catch((err)=>{
                console.error(err);
            });
        },
        async getAttrsForCombo()
        {
            let vm = this;
            await axios.post(ApiUrl.LoadAttrComboItems+'/'+vm.LANG).then((res)=>{
                let data = res.data;
                console.warn(data);
                if(data.status)
                {
                    vm.AttributeItems = data.data;
                }
            }).catch((err)=>{
                console.error(err);
            });
        },
        async registerCategory()
        {
            let vm = this;
            let Model = {};
            Model.PC_ParentId = parseInt(vm.ParentCat);
            Model.PC_Title = vm.CatTitle;
            Model.PC_OrderField = parseInt(vm.CatSortField);
            Model.PC_Status = vm.CatStatus;
            Model.PC_Logo = vm.CatImageName;
            Model.PC_ImgTitle = vm.CatImgTitle;
            Model.PC_PrefixCode = vm.CatPrefixCode;
            Model.PC_Description = vm.CatDescription;
            var se = (vm.LoadSeoFormData());
            if(se.url  == "" || se.url == null || se.url.length < 1)
            {
                MSGHNDL.ERR("Url دسته بندی الزامی میباشد");
                return;
            }
            Model.PC_Seo = JSON.stringify(se);
            Model.PC_Type = 1;
            Model.Lang = vm.LANG;
            let url = ApiUrl.registerPc;
            if(vm.inEditingItem)
            {
                Model.PC_Id = vm.EditSelectedItem.pC_Id;
                Model.Lang = vm.EditSelectedItem.lang;
                url = ApiUrl.updatePc;
            }
            await axios.post(url,Model).then(async (res)=>{
                let data = res.data;
                if(data.status)
                {
                    vm.loadCategories();
                    vm.cancelEditItem();
                    MSGHNDL.OK(data.msg);
                }
                else
                {
                    MSGHNDL.ERR(data.msg);
                }
            }).catch((err)=>{console.warn(err)});
        },
        SeoChanged(data)
        {
            this.seo = data;
        }
    }
}
</script>