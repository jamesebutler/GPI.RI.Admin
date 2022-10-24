     <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            /*<![CDATA[*/


            function ClearItems() {
               // alert(" Clear ");
               var resultsDropDownList = $find("<%=DropDownLineSystemType.ClientID %>");
                //  alert(" Clear again...");
               resultsDropDownList.clearItems();
            }

            //Load the Area 
            function OnClientItemsDropDownAreaRequesting(sender, args) {
                debugger
                //alert("i am here")
            var valueFind = $find('<%=DropDownBusinessUnit.ClientID%>').get_selectedItem().get_value();
            
            var valueFindSites = $find('<%=DropDownSites.ClientID%>').get_selectedItem().get_value();
            //alert(valueFind);
            //alert(valueFindSites);

            var context = args.get_context();
                context["businessunit"] = valueFind;
                context["siteid"] = valueFindSites;

            }


             //Load the Line 
            function OnClientItemsDropDownLineRequesting(sender, args) {
                debugger
                //alert("DropDownLineRequesting")
            var valueFindSites = $find('<%=DropDownSites.ClientID%>').get_selectedItem().get_value();

            var valueFindBusiness = $find('<%=DropDownBusinessUnit.ClientID%>').get_selectedItem().get_value();
            
           var valueFindArea = $find('<%=DropDownArea.ClientID%>').get_selectedItem().get_value();
            
                //alert(valueFindSites);
                //alert(valueFindBusiness);
                //alert(valueFindArea);

                var context = args.get_context();
                context["siteid"] = valueFindSites;
                context["businessunit"] = valueFindBusiness;
                context["area"] = valueFindArea;

            }


            function OnClientItemSelected(sender, eventArgs) {

                var item = eventArgs.get_item();
                //alert("You selected " + item.get_text() + " with value " + item.get_value());


                var dropdownlist = $find("<%= DropDownArea.ClientID %>");
                var selectedItem = dropdownlist.get_selectedItem();
                if (selectedItem) {
                     
                    // following two lines clear the entries
                    dropdownlist.get_textElement().innerHTML = "";
                    dropdownlist.get_items().clear();
                    selectedItem.unselect();
                }

                var dropdownlist = $find("<%= DropDownLineSystemType.ClientID %>");
                var selectedItem = dropdownlist.get_selectedItem();
                if (selectedItem) {
                    selectedItem.unselect();
                }

            }


                /*]]>*/
            </script>
        </telerik:RadScriptBlock>
