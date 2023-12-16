using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AsepStudios.TableChump.UI
{
    [RequireComponent(typeof(ViewRefHolder))]
    public class ViewManager : MonoBehaviour
    {
        private static ViewManager instance;

        public View activeView;
        private ViewRefHolder RefHolder => GetViewSystemRefHolder();
        private ViewRefHolder refHolder;
        private List<View> ViewList => RefHolder.ViewList;
        private View DefaultView => RefHolder.DefaultView;
        private Transform ViewTransform => RefHolder.viewTransform;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            //Initialize();
        }

        public static void Initialize()
        {
            if (instance.DefaultView != null)
            {
                instance.OpenView(instance.DefaultView);
            }
        }

        public static void ShowView<TView>(object args = null) where TView : View
        {
            foreach (var view in instance.ViewList.OfType<TView>())
            {
                if (instance.activeView == null)
                {
                    instance.OpenView(view, args);
                }
                else
                {
                    if (instance.activeView is not TView)
                    {
                        instance.CloseView(instance.activeView);
                        instance.OpenView(view, args);
                    }
                }
            }
        }
     

        public static void HideView()
        {
            if (instance.activeView != null)
            {
                instance.CloseView(instance.activeView);
            }
        }

        private void OpenView(View view, object args = null)
        {
            activeView = Instantiate(view, ViewTransform);
            activeView.PassArgs(args);
        }

        private void CloseView(View view)
        {
            Destroy(view.gameObject);
        }

        private ViewRefHolder GetViewSystemRefHolder()
        {
            if (refHolder == null)
            {
                refHolder = GetComponent<ViewRefHolder>();
            }

            return refHolder;
        }
    }

}